using AquaSolution.Shared.ManageMedicalRooms.WarehouseImports;

namespace AquaSolution.Server.Services.ManageMedicalRooms.WarehouseImportService
{
    public interface IWarehouseImportService
    {                 
        Task<bool> WarehouseImport(CreatedWarehouseImportDto createdWarehouseImportDto);
        Task<List<LoadWarehouseImportDto>> GetWarehouseImport();
        Task<List<LoadWarehouseImportDetailDto>> GetWarehouseImportDetail(Guid warehouseImportId);
    }
}
