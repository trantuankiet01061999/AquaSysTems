
using System.ComponentModel.DataAnnotations;

namespace AquaSolution.Shared.Enum
{
    public enum WarehouseExportType
    {
        [Display(Name = "Xuất đơn thuốc")]
        PrescriptionExportType,
        [Display(Name = "Xuất thuốc theo yêu cầu")]
        ExportOnRequest,
        [Display(Name = "Hủy thuốc hết HSD")]
        DestroyMedicine,
        [Display(Name = "Xuất Điều chỉnh tồn kho")]
        InventoryAdjustment
    }
}
