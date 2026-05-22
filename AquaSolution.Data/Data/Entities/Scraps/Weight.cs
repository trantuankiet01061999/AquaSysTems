using AquaSolution.Shared.Enum.Scrap;

namespace AquaSolution.Data.Data.Entities.Scraps
{
    public class Weight
    {
        public Guid Id { get; set; }
        public Guid MaterialId { get; set; }
        public decimal WeightValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }

    }
}
