using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.ReportDto
{
    public class MaterialReportDto
    {
        public Guid MaterialId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal TotalQuantity { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal ConfirmedWeight { get; set; }
        public decimal Difference => ConfirmedWeight - TotalWeight;
    }

}
