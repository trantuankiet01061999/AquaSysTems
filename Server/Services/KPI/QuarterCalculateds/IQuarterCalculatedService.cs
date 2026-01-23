using AquaSolution.Shared.KPI.KPITasks;
using AquaSolution.Shared.KPI.QuaterCalculated;


namespace AquaSolution.Server.Services.KPi.QuarterCalculateds
{
    public interface IQuarterCalculatedService
    {
        Task<List<QuarterCalculatedDto>> LoadListAsync();
        Task<bool> CreatedAsync(QuarterCalculatedDto formulaDto);
        Task<bool>UpdateAsync(QuarterCalculatedDto formulaDto);
        Task<bool>DeletedAsync(Guid Id);
    }
}
