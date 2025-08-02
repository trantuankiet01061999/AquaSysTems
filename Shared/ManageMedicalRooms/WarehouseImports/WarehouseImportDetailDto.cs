using AquaSolution.Shared.Enum;
using AquaSolution.Shared.ManageMedicalRooms.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.ManageMedicalRooms.WarehouseImports
{
    public class WarehouseImportDetailDto
    {
        public Guid Id { get; set; }
        public DateTime? DateManufacture { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Quantity { get; set; }
        public ProductDto productDto { get; set; } = new();
    }
}
