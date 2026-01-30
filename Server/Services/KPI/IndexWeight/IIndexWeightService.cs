using AquaSolution.Shared.Enum;
using AquaSolution.Shared.Enum.KPIType;
using AquaSolution.Shared.KPI.IndexWeight;
using AquaSolution.Shared.KPI.KPIActual;
using AquaSolution.Shared.KPI.KPISubmit;
using AquaSolution.Shared.KPI.Result;
using AquaSolution.Shared.KPI.UserTask;

namespace AquaSolution.Server.Services.KPI.IndexWeight
{
    public interface IIndexWeightService
    {
        Task<bool> CreatedAsync(IndexWeightDto indexWeightDto);
        Task<List<IndexWeightDto>> GetListAsync();

    }
}
