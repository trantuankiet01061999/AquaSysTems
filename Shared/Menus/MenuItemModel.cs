using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaService.Shared.Menus
{
    public class MenuItemModel
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string? Permission { get; set; } // null nếu không cần phân quyền
    }
}
