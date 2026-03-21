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
