using AquaSolution.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Data.Data.Entities.Admin
{
    public class SystemLock
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public bool IsLocket { get; set; }
        public Guid LockedBy { get; set; }
        public DateTime LockedDate { get; set; }
    }
}
