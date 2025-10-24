using AquaSolution.Shared.Enum;


namespace AquaSolution.Shared.ApprovalFlows
{
    public class ApprovalFlowDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PositionName { get; set; } 
        public Guid? DecisionMaker { get; set; }
        public string? DecisionMakerName { get; set; } 
        public Guid PositionId { get; set; }
        public string? DesCription { get; set; }
        public int? CurrentStep { get; set; }
        public int? NextStep { get; set; }
        public ApprovalSettingType ApprovalSettingType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
