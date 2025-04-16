using coconut_asp_dotnet_back_end.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Getting secrets fot DB access
var CoconutDBHost = builder.Configuration["Coconut:DBHost"];
var CoconutDBuser = builder.Configuration["Coconut:DBuser"];
var CoconutDBpassword = builder.Configuration["Coconut:DBpassword"];

//Setting up PostgresDB context
builder.Services.AddDbContextPool<CoconutContext>(opt =>
    opt.UseNpgsql(
        $"Host={CoconutDBHost};Username={CoconutDBuser};Password={CoconutDBpassword};Database=coconut"
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
