using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.ManageMedicalRooms.WarehouseImports
{
    public class CreatedWarehouseImportDto
    {
        public WarehouseImportDto WarehouseImportDto { get; set; } = new();
        public List<WarehouseImportDetailDto> WarehouseImportDetailDtos { get; set; } = new List<WarehouseImportDetailDto>();
    }
}
