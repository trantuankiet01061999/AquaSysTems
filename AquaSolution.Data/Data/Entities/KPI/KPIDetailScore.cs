using AquaSolution.Shared.Enum;
using AquaSolution.Shared.Enum.KPIType;

namespace AquaSolution.Data.Data.Entities
{
    public class KPIDetailScore
    {
        public Guid Id { get; set; }
        public Guid TotalScoreId { get; set; }
        public Guid TaskId { get; set; }
        public decimal Max { get; set; }
        public decimal Bottom { get; set; }
        public decimal Weight { get; set; }
        public decimal Target { get; set; }
        public decimal Achievement { get; set; }
        public decimal Actual { get; set; }
        public decimal Score { get; set; }
        public int? Month { get; set; }
        public int? Quarter { get; set; }     
        public int? HalfYear { get; set; }    
        public int Year { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
