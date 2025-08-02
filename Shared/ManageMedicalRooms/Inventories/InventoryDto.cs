using AquaSolution.Shared.Enum;

namespace AquaSolution.Shared.ManageMedicalRooms.Inventories
{
    public class InventoryDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public AdditionalSourceType? AdditionalSourceType { get; set; }
        public ProductType? ProductType { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public bool expired { get; set; }
    }
}
