using AquaSolution.Shared.ManageMedicalRooms.Inventories;

namespace AquaSolution.Server.Services.Administration.InventoriesService
{
    public interface IInventoryService
    {
        Task<List<InventoryDto>> LoadListAsync();
       
    }
}
