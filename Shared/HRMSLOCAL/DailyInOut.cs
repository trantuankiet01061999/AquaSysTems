using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.HRMSLOCAL
{
    public class DailyInOut
    {
        public string EmployeeATID { get; set; }
        public string EmployeeName { get; set; }
        public string EmpFactory { get; set; }

        public DateTime InTime { get; set; }
        public DateTime? OutTime { get; set; }

        public DateTime CheckDate { get; set; }
    }
}
