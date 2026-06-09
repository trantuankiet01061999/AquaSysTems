using AquaSolution.Shared.Enum.Scrap;
namespace AquaSolution.Shared.ReportDto
{
    public class ApprovalPipelineDto
    {
        public Guid ScrapId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        public string DecisionMakerName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public StatusScrap Status { get; set; }
    }
}
