using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.BLL.Services;
using SPR411_SteamClone.DAL;
using SPR411_SteamClone.DAL.Initializer;
using SPR411_SteamClone.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add repositories
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<DeveloperRepository>();

// Add services
builder.Services.AddScoped<DeveloperService>();

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

// Add swagger
builder.Services.AddSwaggerGen();

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

// Cors
app.UseCors(corsPolicy);

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.SeedAsync();

app.Run();
