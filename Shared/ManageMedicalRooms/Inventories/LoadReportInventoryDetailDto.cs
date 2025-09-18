using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.ManageMedicalRooms.Inventories
{
    public class LoadReportInventoryDetailDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal BeginningInventory {  get; set; }
        public decimal NewInbound { get; set; }
        public decimal ConsumPosition { get; set;}
        public decimal TotalStock { get; set;}
        public string Unit {  get; set; }

    }
}
