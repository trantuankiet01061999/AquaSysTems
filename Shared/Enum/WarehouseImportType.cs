
using System.ComponentModel.DataAnnotations;

namespace AquaSolution.Shared.Enum
{
    public enum WarehouseImportType
    {
        [Display(Name = "Nhập Điều chỉnh tồn kho")]
        InventoryAdjustment,
        [Display(Name = "Nhập thuốc")]
        AddMoreMedicine,
    }
}
