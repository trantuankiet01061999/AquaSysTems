using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.ReportDto
{
    public class ReportSummaryDto
    {
        public int TotalOrders { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal ConfirmedWeight { get; set; }
        public int PendingOrders { get; set; }
        public int OverduePendingOrders { get; set; }
        public double ConfirmRate => TotalWeight == 0 ? 0 : Math.Round((double)(ConfirmedWeight / TotalWeight) * 100, 1);

        // % thay đổi so kỳ trước (tính ở service)
        public double TotalOrdersChange { get; set; }
        public double TotalWeightChange { get; set; }
    }
}
