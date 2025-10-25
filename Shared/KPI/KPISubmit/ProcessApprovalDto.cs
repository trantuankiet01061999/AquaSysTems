using AquaSolution.Shared.Enum.KPIType;

namespace AquaSolution.Shared.KPI.KPISubmit
{
    public class ProcessApprovalDto
    {
        public Guid RequestApprovalTaskId { get; set; }
        public string? ApprovalEmail { get; set; } 
        public string? ApprovalName { get; set; } 
        public DateTime? ApprovalDate { get; set; }
        public string? RejectedEmail { get; set; }
        public string? RejectedName { get; set; }
        public string? PendingEmail { get; set; }
        public string? PendingName { get; set; }
        public DateTime? RejectedDate { get; set; }
        public EApprovalStatusType ApprovalStatusType { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int StepNumber { get; set; }
    }
}
