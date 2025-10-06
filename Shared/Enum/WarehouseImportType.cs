
using System.ComponentModel.DataAnnotations;

namespace AquaSolution.Shared.Enum
{
    public enum WarehouseImportType
    {
        [Display(Name = "Xuất Điều chỉnh tồn kho")]
        InventoryAdjustment,
        [Display(Name = "Nhập đơn thuốc")]
        AddMoreMedicine,
    }
}
