using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaSolution.Shared.UserManagements
{
    public class RoleDto
    {

            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<string> Permissions { get; set; } = new();
           public bool IsSelected { get; set; }
        
    }
}
