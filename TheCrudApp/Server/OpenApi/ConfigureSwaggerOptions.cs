using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TheCrudApp.Server.OpenApi;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IWebHostEnvironment webHostEnvironment)
    {
        _provider = provider;
        _webHostEnvironment = webHostEnvironment;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }
    
    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "The CRUD App",
            Version = description.ApiVersion.ToString(),
            Description = $"""
                           Environment: {_webHostEnvironment.EnvironmentName}{Environment.NewLine}
                           """
        };
        
        if (description.IsDeprecated)
        {
            info.Description += "This API version has been deprecated.";
        }
        
        return info;
    }
}