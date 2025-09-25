using apiserasa.domain.interfaces;
using apiserasa.domain.services;
using apiserasa.infra.data;
using apiserasa.infra.interfaces;
using apiserasa.infra.repositories;
using apiserasa.routes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.UseUrls("http://0.0.0.0:5000");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowHost", policy =>
        policy.WithOrigins("http://127.0.0.1:5500")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var dbPath = Path.Combine(AppContext.BaseDirectory, "database.db");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IPetRepository, PetRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerUI();
app.UseSwagger();

app.UseHttpsRedirection();

app.UseCors("AllowHost");

app.UserRoutes();
app.PetRoutes();

app.Run();
