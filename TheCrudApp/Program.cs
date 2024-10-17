using System.Text.Json;
using System.Text.Json.Serialization;
using Asp.Versioning;
using TheCrudApp.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = builder.Environment.IsDevelopment();
    options.ValidateOnBuild = builder.Environment.IsDevelopment();
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.AddHealthChecks();
builder.AddOpenApi();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true; 
});

builder.AddDatabase()
    .AddRepositories()
    .AddServices();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseExceptionHandler();
app.UseStaticFiles();
app.UseRouting();

app.UseOpenApi();
app.MapControllers();
app.MapHealthChecks();

app.Map("/", () => Results.LocalRedirect("/swagger"));

app.Run();