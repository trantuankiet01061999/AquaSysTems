using AquaSolution.Shared.UserManagements;

namespace AquaSolution.Server.Services.Administration.RolePermissionService
{
    public interface IRolePermissionService
    {
        Task<int> UpdateRolePermission(Guid roleId, List<Guid> SelectedPermissions);
        Task<RoleDto> GetRoleWithPermissions(Guid roleId);
    }
}
