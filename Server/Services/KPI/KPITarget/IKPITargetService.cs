using AquaSolution.Shared.KPI.KPIMonthlyTarget;
using AquaSolution.Shared.KPI.KPITarget;
using AquaSolution.Shared.KPI.KPITasks;


namespace AquaSolution.Server.Services.KPi.KPITaskService
{
    public interface IKPIMonthlyTargetService
    {
        Task<List<GetUserTaskAndTargetDto>> GetTargetByTask(Guid userTaskId, Guid userId);
        Task<List<TargetDto>> GetTargetByUser(Guid userId);
    }
}
