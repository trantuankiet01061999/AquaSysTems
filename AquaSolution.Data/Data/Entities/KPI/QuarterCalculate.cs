using AquaSolution.Shared.Enum.KPIType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Data.Data.Entities.KPI
{
    public class QuarterCalculate
    {
        public Guid Id { get; set; }
        public string QuarterCalculated { get; set; }
        public string Description { get; set; }
        public QuarterCalculateType QuarterCalculateType { get; set; }
    }
}
