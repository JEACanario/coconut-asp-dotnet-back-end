using coconut_asp_dotnet_back_end.Data;
using coconut_asp_dotnet_back_end.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //CORS Policy to stop me from hurting myself
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                name: "CORSPolicy",
                policy =>
                {
                    policy
                        .WithOrigins(
                            "http://localhost:5173",
                            "https://localhost:5173",
                            "http://127.0.0.1:5173",
                            "https://127.0.0.1:5173"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
            );
        });

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


        //builder.Services.AddAuthorizationBuilder();


        // Activate Identity APIs & usermanager for AppUserController
        builder
            .Services.AddIdentityApiEndpoints<AppUser>()
            .AddEntityFrameworkStores<CoconutContext>()
            .AddUserManager<UserManager<AppUser>>();

        builder.Services.AddAuthorization();

        // Cookie settings for cross-site requests
        /*       builder.Services.Configure<CookieAuthenticationOptions>(
                  IdentityConstants.ApplicationScheme,
                  options =>
                  {
                      options.Cookie.SameSite = SameSiteMode.None;
                      options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                  }
              );
       */
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
            app.UseCors("CORSPolicy");
            /* app.UseCors(x =>
                x.WithOrigins(
                        "http://localhost:5173",
                        "https://localhost:5173",
                        "http://127.0.0.1:5173",
                        "https://127.0.0.1:5173"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ) ;*/
            app.MapOpenApi();
            app.MapGet(
                "/debug/routes",
                (IEnumerable<EndpointDataSource> endpointSources) =>
                    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints))
            );
            Console.WriteLine("API Mapped");
        }

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<CoconutContext>();

            DbInitializer.Initialize(context);
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
