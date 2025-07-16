using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaService.Shared.Permissions
{
    public static class Permissions
    {
        public const string Admin = "Role.Admin";
        public const string UserView = "User.View";
        public const string UserCreate = "User.Create";
        public const string UserEdit = "User.Edit";
        public const string UserDelete = "User.Delete";
        public static List<string> GetAllPermissions()
        {
            return new List<string>
                {
                    UserView,
                    UserCreate,
                    UserEdit,
                    UserDelete
                };
        }
    }

}
