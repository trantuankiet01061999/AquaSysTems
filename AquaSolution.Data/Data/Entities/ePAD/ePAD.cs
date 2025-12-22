using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Data.Data.Entities.ePAD
{

    [Table("IC_AttendanceLog")]
    public class ePAD
    {
        public string EmployeeATID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime CheckTime { get; set; }
    }

}
