using System.Reflection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using TheCrudApp.Database;
using TheCrudApp.Database.Repositories;
using TheCrudApp.Server.OpenApi;

namespace TheCrudApp.Server;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder app)
    {
        
        app.Services.AddSingleton<TimeProvider>(TimeProvider.System);
        return app;
    }

    public static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder app)
    {
        app.Services.AddScoped<ICarRepository, CarRepository>();
        return app;
    }
    
    public static IHostApplicationBuilder AddDatabase(this IHostApplicationBuilder app)
    {
        app.Services.Configure<DatabaseConfig>(app.Configuration.GetSection("Database"));
        
        app.Services.AddDbContextPool<DatabaseContext>((provider, optionsBuilder) =>
        {
            var connectionString = provider.GetRequiredService<IOptions<DatabaseConfig>>().Value.ConnectionString;
            optionsBuilder.UseNpgsql(connectionString, builder =>
                {
                    builder.MigrationsHistoryTable($"__{nameof(DatabaseContext)}_Migrations");
                })
                .EnableSensitiveDataLogging();
        });

        return app;
    }
    
    public static IHostApplicationBuilder AddOpenApi(this IHostApplicationBuilder app)
    {
        app.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        app.Services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.DocumentFilter<SortByPathFilter>();
        });

        return app;
    }

    public static WebApplication UseOpenApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();
            foreach (var description in descriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
            options.InjectStylesheet("./dark-mode.css");
            options.EnableDeepLinking();
            options.DefaultModelExpandDepth(3);
            options.DefaultModelsExpandDepth(3);
            options.DisplayRequestDuration();
            options.EnableFilter();
            options.EnableTryItOutByDefault();
        });

        return app;
    }

    public static IHostApplicationBuilder AddHealthChecks(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck("livenessProbe", () => HealthCheckResult.Healthy(), ["live"])
            .AddCheck("readinessProbe", () => HealthCheckResult.Healthy(), ["ready"]);

        return builder;
    }
    
    public static WebApplication MapHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");
        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live")
        });
        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("ready")
        });

        return app;
    }
}