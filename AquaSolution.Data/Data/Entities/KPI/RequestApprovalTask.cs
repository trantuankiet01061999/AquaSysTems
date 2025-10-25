
using AquaSolution.Shared.Enum.KPIType;

namespace AquaSolution.Data.Data.Entities.KPI
{
    public class RequestApprovalTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } =string.Empty;
        public Guid SubmitId { get; set; }
        public Guid RequesterId { get; set; }
        public EApprovalStatusType StatusType { get; set; }
        public string? Comment { get; set; }
        public  Guid? ApprovedBy { get; set; }
        public  DateTime? ApprovalDate { get; set; }
        public Guid? RejectBy { get; set; }
        public DateTime? RejectDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? Step { get; set; }
        public Guid? DecisionMaker { get; set; }
        public int Month { get; set; }
    }
}
