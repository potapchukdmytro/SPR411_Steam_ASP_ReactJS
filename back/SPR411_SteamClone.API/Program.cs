using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SPR411_SteamClone.BLL.Services;
using SPR411_SteamClone.BLL.Settings;
using SPR411_SteamClone.DAL;
using SPR411_SteamClone.DAL.Entities;
using SPR411_SteamClone.DAL.Initializer;
using SPR411_SteamClone.DAL.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add repositories
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<DeveloperRepository>();
builder.Services.AddScoped<GameRepository>();
builder.Services.AddScoped<GameImageRepository>();

// Add services
builder.Services.AddScoped<DeveloperService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();

// Add automapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg5NTE2ODAwIiwiaWF0IjoiMTc1ODAwNDY5MiIsImFjY291bnRfaWQiOiIwMTk5NTEzZTdlYmY3YjYwOGI4Y2I3NTI3YTE3ZTI5MyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazU4a3hoZXN2ZWI3aDZncms2MHBrYXJrIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.OMUeI0YxSQYUSUYehr5O6yevTWgsGamrSrCFSZ7Sd3fNsl01WU-pr6M6wusxNSxoQ6w8-lqrjOk6gj8KShQQhmvz91wRuRm_rObvAaDQEBRDit7iSUe6J7EH8lDmpqlUuJQ8zN0lCTgIDwaHDaI9h4FcSVy6qmi68oETGI876KCUf5ifCCwDSpZjirIws5XvO6IpQEkCp8FWd2UkTWvrHaaJWFbxOWfKbx_j5AeHPE1o5Piiz7qF6QKX8MzOj44f0yRExRKMCeQSauqRBgO33CooOm0mxbU2-Mx5tb3PPHdaFe7YxPKdRYSJ1TsRn3DELSrxnKsPE11X4eIXYuJh6w";
}, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("LocalDb");
    options.UseNpgsql(connectionString);
});

// Identity
builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Options
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Add authentication
string? secretKey = builder.Configuration["JwtSettings:SecretKey"];
if (string.IsNullOrEmpty(secretKey))
{
    throw new ArgumentNullException("Jwt secret key is null");
}

byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
string issuer = builder.Configuration["JwtSettings:Issuer"] ?? string.Empty;
string audience = builder.Configuration["JwtSettings:Audience"] ?? string.Empty;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes)
    };
});

// Add swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SPR411", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add cors
string corsPolicy = "allowAll";
builder.Services.AddCors(cfg =>
{
    cfg.AddPolicy(corsPolicy, cfg =>
    {
        cfg.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Static files
var root = builder.Environment.ContentRootPath;
var storagePath = Path.Combine(root, StaticFilesSettings.Storage);
var sharePath = Path.Combine(storagePath, StaticFilesSettings.Share);
var developersPath = Path.Combine(storagePath, StaticFilesSettings.Developers);
var gamesPath = Path.Combine(storagePath, StaticFilesSettings.Games);

if (!Directory.Exists(storagePath))
{
    Directory.CreateDirectory(storagePath);
}

if (!Directory.Exists(sharePath))
{
    Directory.CreateDirectory(sharePath);
}

if (!Directory.Exists(developersPath))
{
    Directory.CreateDirectory(developersPath);
}

if (!Directory.Exists(gamesPath))
{
    Directory.CreateDirectory(gamesPath);
}

// Share
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(sharePath),
    RequestPath = "/share"
});

// Developers
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(developersPath),
    RequestPath = "/developer"
});

// Games
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(gamesPath),
    RequestPath = "/game"
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(corsPolicy);

await app.SeedAsync();

app.Run();
