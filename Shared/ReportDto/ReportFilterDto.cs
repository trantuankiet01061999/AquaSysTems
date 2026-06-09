using AquaSolution.Shared.Enum.Scrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.ReportDto
{
    public class ReportFilterDto
    {
        public Guid? FactoryId { get; set; }
        public FilterPeriod Period { get; set; } = FilterPeriod.Month;
        public int Year { get; set; } = DateTime.Now.Year;
        public int? Month { get; set; } = DateTime.Now.Month;
        public int? Week { get; set; }
    }
}
