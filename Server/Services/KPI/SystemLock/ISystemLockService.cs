using AquaSolution.Shared.Administration.SystemLock;
using AquaSolution.Shared.KPI.DealineManagement;
using AquaSolution.Shared.KPI.Formula;
using AquaSolution.Shared.ManageMedicalRooms.Inventories;
using Microsoft.AspNetCore.Authentication;

namespace AquaSolution.Server.Services.Administration.SystemLock
{
    public interface ISystemLockService
    {
        Task<List<SystemLockDto>> LoadDataAsync();
        Task<bool>CreatedAsync(SystemLockDto systemLockDto);
        Task<bool>UpdateStatus(Guid systemLockId, bool isLocket);
        Task<bool> CheckLock(Guid pageId);
    }
}
