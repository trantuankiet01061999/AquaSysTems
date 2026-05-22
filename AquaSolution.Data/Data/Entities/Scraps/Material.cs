using AquaSolution.Shared.Enum.Scrap;

namespace AquaSolution.Data.Entities.Scraps
{
    public class Material
    {
        public Guid Id { get; set; }
        public string BOMHead { get; set; }
        public string BOMDescription { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TYPE { get; set; }
        public string Unit { get; set; }
        public PlantType Plant { get;  set;  }

    }
}
