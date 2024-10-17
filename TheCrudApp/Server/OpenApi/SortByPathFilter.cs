using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TheCrudApp.Server.OpenApi;

public class SortByPathFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths.OrderBy(e => e.Key).ToList();

        var pathsDictionary = new OpenApiPaths();
        foreach (var (path, value) in paths)
        {
            pathsDictionary[path] = value;
        }

        swaggerDoc.Paths = pathsDictionary;
    }
}