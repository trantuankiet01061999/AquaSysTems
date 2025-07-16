using AquaService.Shared;
using AquaService.Shared.Permissions;
using AquaSolution.Shared.UserManagements;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AquaService.Server.Services;

public class TokenServiceMock
{
    private static readonly Dictionary<string, (string Password, List<string> Roles, string FullName)> users = new()
{
    // Admin users
    { "admin", ("admin123", new List<string> { "Admin" }, "Quản trị viên") },
    { "superadmin", ("super123", new List<string> { "Admin" }, "Quản trị cấp cao") },

    // Manager users (thêm cả Admin role)
    { "manager", ("manager123", new List<string> { "Manager", "Admin" }, "Quản lý trung tâm") },

    { "salesmanager", ("sales123", new List<string> { "Manager" }, "Quản lý kinh doanh") },
    { "techmanager", ("tech123", new List<string> { "Manager" }, "Quản lý kỹ thuật") },

    // Staff
    { "staff1", ("staff123", new List<string> { "Staff" }, "Nhân viên kinh doanh") },
    { "staff2", ("staff123", new List<string> { "Staff" }, "Nhân viên hỗ trợ") },

    // Support
    { "support1", ("support123", new List<string> { "Support" }, "Hỗ trợ khách hàng 1") },
    { "support2", ("support123", new List<string> { "Support" }, "Hỗ trợ khách hàng 2") },

    // Users
    { "user1", ("user123", new List<string> { "User" }, "Người dùng thường 1") },
    { "user2", ("user123", new List<string> { "User" }, "Người dùng thường 2") },
    { "premiumuser", ("premium123", new List<string> { "User" }, "Người dùng cao cấp") }
};


    private const string SecretKey = "ThisIsASuperSecureSecretKey12345678"; // >= 32 ký tự

    public string? GenerateToken(string username, string password)
    {
        if (!users.TryGetValue(username, out var userInfo) || userInfo.Password != password)
            return null;

        var claims = new List<Claim>
    {
        new(ClaimTypes.Name, username),
        new("FullName", userInfo.FullName)
    };

        // Thêm tất cả role
        foreach (var role in userInfo.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));

            // Thêm permission theo từng role
            switch (role)
            {
                case "Admin":
                    claims.AddRange(new[]
                    {
                    new Claim("permission", Permissions.UserView),
                    new Claim("permission", Permissions.UserCreate),
                    new Claim("permission", Permissions.UserEdit),
                    new Claim("permission", Permissions.UserDelete)
                });
                    break;

                case "Manager":
                    claims.AddRange(new[]
                    {
                    new Claim("permission", Permissions.UserView),
                    new Claim("permission", Permissions.UserEdit)
                });
                    break;

                case "Staff":
                case "Support":
                case "User":
                    claims.Add(new Claim("permission", Permissions.UserView));
                    break;
            }
        }

        // Sinh token sau khi xử lý tất cả role
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public static IEnumerable<RoleDto> GetRoles()
    {
        return new List<RoleDto>
        {
            new RoleDto
            {
                Id = "1",
                Name = "Admin",
                Description = "Quản trị viên hệ thống",
                Permissions = new List<string>
                {
                    Permissions.UserView,
                    Permissions.UserCreate,
                    Permissions.UserEdit,
                    Permissions.UserDelete
                }
            },
            new RoleDto
            {
                Id = "2",
                Name = "Manager",
                Description = "Quản lý",
                Permissions = new List<string>
                {
                    Permissions.UserView,
                    Permissions.UserEdit
                }
            },
            new RoleDto
            {
                Id = "3",
                Name = "Staff",
                Description = "Nhân viên",
                Permissions = new List<string>
                {
                    Permissions.UserView
                }
            },
            new RoleDto
            {
                Id = "4",
                Name = "Support",
                Description = "Hỗ trợ khách hàng",
                Permissions = new List<string>
                {
                    Permissions.UserView
                }
            },
            new RoleDto
            {
                Id = "5",
                Name = "User",
                Description = "Người dùng thường",
                Permissions = new List<string>
                {
                    Permissions.UserView
                }
            }
        };
    }

    public static IEnumerable<UserDto> GetMockUsers()
    {
        return users.Select(u => new UserDto
        {
            Id = Guid.NewGuid().ToString(),
            UserName = u.Key,
            Email = $"{u.Key}@example.com",
            FullName = u.Value.FullName,
            Roles = u.Value.Roles
        });
    }

}
