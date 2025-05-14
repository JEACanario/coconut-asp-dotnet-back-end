using coconut_asp_dotnet_back_end.Data;
using coconut_asp_dotnet_back_end.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // Getting secrets fot DB access
        var CoconutDBHost = builder.Configuration["Coconut:DBHost"];
        var CoconutDBuser = builder.Configuration["Coconut:DBuser"];
        var CoconutDBpassword = builder.Configuration["Coconut:DBpassword"];

        //Setting up PostgresDB context for Models using Coconut Context
        builder.Services.AddDbContextPool<CoconutContext>(opt =>
            opt.UseNpgsql(
                $"Host={CoconutDBHost};Username={CoconutDBuser};Password={CoconutDBpassword};Database=coconut",
                o => o.SetPostgresVersion(13, 0)
            )
        );

        // Add Identity services to the container
        builder.Services.AddAuthorization();

        // Activate Identity APIs
        builder
            .Services.AddIdentityApiEndpoints<AppUser>()
            .AddEntityFrameworkStores<CoconutContext>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }

        //Add Identity Routes

        app.MapIdentityApi<AppUser>();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            Console.WriteLine("API Mapped");
        }

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<CoconutContext>();

            DbInitializer.Initialize(context);
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
