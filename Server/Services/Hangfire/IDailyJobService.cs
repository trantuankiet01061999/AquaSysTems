namespace AquaSolution.Server.Services.Hangfire
{
    public interface IDailyJobService
    {
        Task RunDailyAsync();
    }
}
