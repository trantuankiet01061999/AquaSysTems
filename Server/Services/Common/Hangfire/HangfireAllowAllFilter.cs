using Hangfire.Annotations;
using Hangfire.Dashboard;
namespace AquaSolution.Server.Services.Common.Hangfire;

public class HangfireAllowAllFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        return true;
    }
}