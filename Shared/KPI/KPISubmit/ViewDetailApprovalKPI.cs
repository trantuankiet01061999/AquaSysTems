using AquaSolution.Shared.Enum.KPIType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.KPI.KPISubmit
{
    public class ViewDetailApprovalKPI
    {
        public List<TotalScoreDto> TotalScore { get; set; } = new List<TotalScoreDto> ();
        public List<DetailScoreDto> DetailScore { get; set; } = new List<DetailScoreDto>();
    }
}
