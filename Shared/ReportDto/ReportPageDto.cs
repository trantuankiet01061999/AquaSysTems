using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.ReportDto
{
    public class ReportPageDto
    {
        public ReportSummaryDto Summary { get; set; } = new();
        public List<DepartmentReportDto> DepartmentReport { get; set; } = new();
        public List<MaterialReportDto> MaterialReport { get; set; } = new();
        public List<TrendPointDto> Trend { get; set; } = new();
        public ApprovalStatusDto ApprovalStatus { get; set; } = new();
        public List<ApprovalPipelineDto> Pipeline { get; set; } = new();
    }
}
