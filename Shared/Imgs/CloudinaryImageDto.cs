using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.Imgs
{

    public class CloudinaryImageDto
    {
        public string PublicId { get; set; }
        public string SecureUrl { get; set; }
        public decimal FileSize { get; set; }
        public string FileSizeUnit { get; set; }
        public DateTime UpLoadDate { get; set; }
        public string WorkId { get; set; } = string.Empty;
    }

}
