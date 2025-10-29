using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.KPI.KPISubmit
{
    public class ApprovalInfo
    {
        public Guid SubmitId { get; set; }
        public Guid RequestTaskId { get; set; }
        public Guid? DecisionMaker { get; set; }
        public bool IsApproved { get; set; }
        public string ? Comments { get; set; }
    }
}
