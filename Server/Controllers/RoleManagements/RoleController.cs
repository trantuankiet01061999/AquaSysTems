using AquaService.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaSolution.Server.Controllers.UserManagements
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // Yêu cầu đăng nhập
    public class RolesController : ControllerBase
    {
        [HttpGet("get-all")]
        public IActionResult GetAllRoles()
        {
            try
            {
                var roles = TokenServiceMock.GetRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách role", Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(string id)
        {
            var role = TokenServiceMock.GetRoles().FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
    }
}
