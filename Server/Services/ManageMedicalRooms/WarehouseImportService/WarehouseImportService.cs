using AquaSolution.Data.Connection;
using AquaSolution.Data.Data.Entities;
using AquaSolution.Data.Repositories;
using AquaSolution.Shared.ManageMedicalRooms.WarehouseImports;

namespace AquaSolution.Server.Services.ManageMedicalRooms.WarehouseImportService
{
    public class WarehouseImportService : IWarehouseImportService
    {
        private readonly IRepository<Inventories> _inventoryRepo;
        private readonly IRepository<WarehouseImport> _warehouseImportRepo;
        private readonly IRepository<WarehouseImportDetail> _warehouseImportDetailRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly AquaDbContext _context;

        public WarehouseImportService
            (
            IRepository<Inventories> inventoryRepo,
             IRepository<WarehouseImport> warehouseImportRepo,
             IRepository<Product> productRepo,
                AquaDbContext context,
             IRepository<WarehouseImportDetail> warehouseImportDetailRepo

            )
        {
            _inventoryRepo = inventoryRepo;
            _context = context;
            _warehouseImportRepo = warehouseImportRepo;
            _warehouseImportDetailRepo = warehouseImportDetailRepo;
            _productRepo = productRepo;
        }

        public async Task<List<LoadWarehouseImportDto>> GetWarehouseImport()
        {
            var warehouseImportQuery = from warehouseImport in await _warehouseImportRepo.GetQueryableAsync()
                                       select new LoadWarehouseImportDto
                                       {
                                           Id = warehouseImport.Id,
                                           Code = warehouseImport.Code,
                                           Name = warehouseImport.Name,
                                           Description = warehouseImport.Description,
                                           Note = warehouseImport.Note,
                                           CreatedDate = warehouseImport.CreatedDate,
                                           CreatedBy = warehouseImport.CreatedBy,
                                           UpdatedBy = warehouseImport.UpdatedBy,
                                           UpdatedDate = warehouseImport.UpdatedDate,
                                           WarehouseImportType = warehouseImport.WarehouseImportType,
                                       };
            var datareturn = warehouseImportQuery.OrderBy(x=>x.CreatedDate).ToList();
            if (datareturn.Any()) { return datareturn; }
            return new List<LoadWarehouseImportDto>();
        }

        public async Task<List<LoadWarehouseImportDetailDto>> GetWarehouseImportDetail(Guid warehouseImportId)
        {
            var warehouseImportDetailQuery = from warehouseImportDetail in await _warehouseImportDetailRepo.GetQueryableAsync()
                                             join product in await _productRepo.GetQueryableAsync()
                                             on warehouseImportDetail.ProductId equals product.Id
                                             where warehouseImportDetail.WarehouseImportId == warehouseImportId
                                             select new LoadWarehouseImportDetailDto
                                             {
                                                 Id = warehouseImportDetail.Id,
                                                 ProductId = product.Id,
                                                 ProductName = product.Name,
                                                 DateManufacture = warehouseImportDetail.DateManufacture,
                                                 ExpiryDate = warehouseImportDetail.ExpiryDate,
                                                 Quantity = warehouseImportDetail.Quantity,
                                                 ProductType = warehouseImportDetail.ProductType,
                                                 Unit = product.Unit,
                                             };
            var dataReturn = warehouseImportDetailQuery.OrderBy(x => x.ProductName).ToList();
                if(dataReturn.Any()) { return dataReturn; }
            return new List<LoadWarehouseImportDetailDto>();
        }

        public async Task<bool> WarehouseImport(CreatedWarehouseImportDto createdWarehouseImportDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var inventoryQuery = await _inventoryRepo.GetQueryableAsync();

                var warehouseImport = new WarehouseImport
                {
                    Id = Guid.NewGuid(),
                    Code = createdWarehouseImportDto.WarehouseImportDto.Code,
                    Name = createdWarehouseImportDto.WarehouseImportDto.Name,
                    Description = createdWarehouseImportDto.WarehouseImportDto.Description,
                    Note = createdWarehouseImportDto.WarehouseImportDto.Note,
                    CreatedDate = DateTime.Now,
                    CreatedBy = createdWarehouseImportDto.WarehouseImportDto.CreatedBy
                };

                await _warehouseImportRepo.InsertAsync(warehouseImport);

                foreach (var item in createdWarehouseImportDto.WarehouseImportDetailDtos)
                {
                    var detail = new WarehouseImportDetail
                    {
                        Id = item.Id,
                        ProductId = item.productDto.Id,
                        WarehouseImportId = warehouseImport.Id,
                        DateManufacture = item.DateManufacture,
                        ExpiryDate = item.ExpiryDate,
                        Quantity = item.Quantity,
                        ProductType = item.productDto.ProductType
                    };

                    await _warehouseImportDetailRepo.InsertAsync(detail);

                    var existingInventory = inventoryQuery.FirstOrDefault(i =>
                        i.ProductId == item.productDto.Id &&
                        i.ExpirationDate.HasValue &&
                        i.ExpirationDate.Value.Date == item.ExpiryDate.Value.Date &&
                        (
                            !item.DateManufacture.HasValue && !i.ManufacturingDate.HasValue ||
                            item.DateManufacture.HasValue && i.ManufacturingDate.HasValue &&
                            i.ManufacturingDate.Value.Date == item.DateManufacture.Value.Date
                        ));

                    if (existingInventory != null)
                    {
                        existingInventory.Quantity += item.Quantity;
                        await _inventoryRepo.UpdateAsync(existingInventory);
                    }
                    else
                    {
                        var newInventory = new Inventories
                        {
                            Id = Guid.NewGuid(),
                            ProductId = item.productDto.Id,
                            Quantity = item.Quantity,
                            ExpirationDate = item.ExpiryDate,
                            ManufacturingDate = item.DateManufacture
                        };
                        await _inventoryRepo.InsertAsync(newInventory);
                    }
                }

                // ✅ Gộp save changes lại
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
