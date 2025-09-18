using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.ManageMedicalRooms.Inventories
{
    public class CreatedReportInventoryDto
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<LoadReportInventoryDetailDto> LoadReportInventoryDetail { get;set; } = new List<LoadReportInventoryDetailDto>();
    }
}
