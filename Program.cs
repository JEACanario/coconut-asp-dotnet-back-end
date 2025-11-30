using System.Text;
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
                            "https://127.0.0.1:5173",
                            "https://coconut-gtcrbzgmckekarcb.westeurope-01.azurewebsites.net/",
                            "http://coconut-gtcrbzgmckekarcb.westeurope-01.azurewebsites.net/",
                            "https://coconut.spot/",
                            "http://coconut.spot/",
                            "https://www.coconut.spot/",
                            "http://www.coconut.spot/"
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

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContextPool<CoconutContext>(opt =>
                opt.UseNpgsql(
                    $"Host={CoconutDBHost};Username={CoconutDBuser};Password={CoconutDBpassword};Database=coconut",
                    o => o.SetPostgresVersion(13, 0)
                )
            );
        }
        else
        {
            var con = Environment.GetEnvironmentVariable(
                "CUSTOMCONNSTR_AZURE_POSTGRESQL_CONNECTIONSTRING"
            );
            builder.Services.AddDbContextPool<CoconutContext>(opt =>
                opt.UseNpgsql(con, o => o.SetPostgresVersion(14, 0))
            );
        }

        // Add Identity services to the container


        //builder.Services.AddAuthorizationBuilder();


        // Activate Identity APIs & usermanager for AppUserController
        builder
            .Services.AddIdentityApiEndpoints<AppUser>()
            .AddEntityFrameworkStores<CoconutContext>()
            .AddUserManager<UserManager<AppUser>>();

        builder.Services.AddAuthorization();

        // Cookie settings for cross-site requests
        builder.Services.PostConfigure<CookieAuthenticationOptions>(
            IdentityConstants.ApplicationScheme,
            o =>
            {
                o.Cookie.SameSite = SameSiteMode.None;
                o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            }
        );
        builder.Services.PostConfigure<CookieAuthenticationOptions>(
            IdentityConstants.ExternalScheme,
            o =>
            {
                o.Cookie.SameSite = SameSiteMode.None;
                o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            }
        );
        builder.Services.PostConfigure<CookieAuthenticationOptions>(
            IdentityConstants.TwoFactorUserIdScheme,
            o =>
            {
                o.Cookie.SameSite = SameSiteMode.None;
                o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            }
        );

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
                {
                    var sb = new StringBuilder();
                    var endpoints = endpointSources.SelectMany(es => es.Endpoints);
                    foreach (var endpoint in endpoints)
                    {
                        if (endpoint is RouteEndpoint routeEndpoint)
                        {
                            sb.AppendLine($"Route: {routeEndpoint.RoutePattern.RawText}");
                            sb.AppendLine(
                                $"Path Segments: {string.Join(", ", routeEndpoint.RoutePattern.PathSegments)}"
                            );
                            sb.AppendLine(
                                $"Parameters: {string.Join(", ", routeEndpoint.RoutePattern.Parameters)}"
                            );
                            sb.AppendLine(
                                $"Inbound Precedence: {routeEndpoint.RoutePattern.InboundPrecedence}"
                            );
                            sb.AppendLine(
                                $"Outbound Precedence: {routeEndpoint.RoutePattern.OutboundPrecedence}"
                            );
                        }

                        var routeNameMetadata = endpoint
                            .Metadata.OfType<Microsoft.AspNetCore.Routing.RouteNameMetadata>()
                            .FirstOrDefault();
                        sb.AppendLine($"Route Name: {routeNameMetadata?.RouteName}");

                        var httpMethodsMetadata = endpoint
                            .Metadata.OfType<HttpMethodMetadata>()
                            .FirstOrDefault();
                        sb.AppendLine(
                            $"HTTP Methods: {string.Join(", ", httpMethodsMetadata?.HttpMethods ?? Array.Empty<string>())}"
                        );
                    }
                    return sb.ToString();
                }
            );
            app.MapGet(
                "/debug/routes2",
                (IEnumerable<EndpointDataSource> endpointSources) =>
                    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints))
            );
            Console.WriteLine("API Mapped");
        }
        else
        {
            app.UseCors("CORSPolicy");
        }

        //basic seeding
        if (builder.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<CoconutContext>();

                DbInitializer.Initialize(context);
            }
        }

        app.UseHttpsRedirection();
        app.UseCookiePolicy(
            new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
                Secure = CookieSecurePolicy.Always,
            }
        );

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
