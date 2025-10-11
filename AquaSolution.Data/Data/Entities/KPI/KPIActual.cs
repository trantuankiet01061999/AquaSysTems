namespace AquaSolution.Data.Data.Entities
{
    public class KPIActual
    {
        public Guid Id { get; set; }
        public Guid KPITargetId { get; set; }
        public Guid KPITotalScoreId { get; set; }
        public int Month { get; set; }  
        public int Quater { get; set; }
        public int HaftYear { get; set; }
        public int Year { get; set; }
        public decimal TargetValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
