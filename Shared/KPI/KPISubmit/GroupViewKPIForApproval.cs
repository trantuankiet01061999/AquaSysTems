using AquaSolution.Shared.Enum.KPIType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.KPI.KPISubmit
{
    public class GroupViewKPIForApproval
    {
        public EApprovalStatusType ApprovalStatusType { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public List<ViewKPIForApprovalDto> Items { get; set; } = new();
    }
}
