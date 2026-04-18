using AquaSolution.Shared.Enum.Scrap;

namespace AquaSolution.Data.KPI.Entities.Scraps
{
    public class Weight
    {
        public Guid Id { get; set; }
        public string BomHeader { get; set; }
        public string BOMDescription { get; set; }
        public string Type { get; set; }
        public string Component { get; set; }
        public string Unit { get; set; }
        public string ComponentDescription { get; set; }
        public PlantType Plant { get;  set;  }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
