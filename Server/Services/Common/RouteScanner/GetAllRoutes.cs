
using Microsoft.AspNetCore.Components;
using System.Reflection;
namespace AquaSolution.Server.Services.Common.RouteScanner;

public static class GetAllRoutes 
{

    public static List<string> GetAllRouter()
    {
        var assembly = typeof(Program).Assembly;
        var reports = assembly
            .GetTypes()
            .Where(t => typeof(System.ComponentModel.IComponent).IsAssignableFrom(t))
            .SelectMany(t => t.GetCustomAttributes<RouteAttribute>())
            .Select(r => r.Template)
            .Distinct()
            .ToList();
        return reports;
    }


}