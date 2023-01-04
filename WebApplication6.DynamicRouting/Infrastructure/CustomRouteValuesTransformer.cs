using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Routing;

public class CustomRouteValuesTransformer : DynamicRouteValueTransformer
{
    public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
    {
        var path = httpContext.Request.Path.Value ?? string.Empty;
        Debug.WriteLine($"Path: {path}");

        values ??= new();
        values.Clear();
        values.Add("controller", "DynamicRoutes");
        values.Add("action", "Index");
        values.Add("fullPath", path);

        return ValueTask.FromResult(values);
    }
}