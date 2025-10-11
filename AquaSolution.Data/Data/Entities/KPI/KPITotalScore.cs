namespace AquaSolution.Data.Data.Entities
{
    public class KPITotalScore
    {
        public Guid Id { get; set; }
        public Guid KPIRequestId { get; set; }
        public decimal KPIScore { get; set; }
        public decimal KeyTaskScore { get; set; }
        public decimal OMGScore { get; set; }
        public Guid CreatedBy { get; set; }
        public int? Month { get; set; }
        public int? Quarter { get; set; }    
        public int? HalfYear { get; set; }   
        public int Year { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
