using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.Administration.SystemLock
{
    public class SystemLockDto
    {
        public Guid Id { get; set; }
        public Guid? PageId { get; set; }
        public string? PageName { get; set; }

        public bool IsLocket { get; set; }
        public Guid LockedBy { get; set; }
        public DateTime? LockedDate { get; set; }
        public string? LockedByName { get; set; }
    }
}
