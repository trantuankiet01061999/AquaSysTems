using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.SemiReport
{
    public class PcbReportDto
    {
        public string? PcbCode { get; set; } = string.Empty;
        public DateTime? ScanTime { get; set; }
        public string? IntermediateCode { get; set; }
    }
}
