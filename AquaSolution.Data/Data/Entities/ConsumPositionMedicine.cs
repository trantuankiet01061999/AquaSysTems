using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Data.Data.Entities
{
    public class ConsumPositionMedicine
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal QuantityUsed { get; set; }
        public DateTime ConsumptionDate { get; set; }
    }
}
