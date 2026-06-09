using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.ReportDto
{
    public class TrendPointDto
    {
        public string Label { get; set; } = string.Empty;    
        public int TotalOrders { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal ConfirmedWeight { get; set; }
    }
}
