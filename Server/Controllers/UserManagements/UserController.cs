using AquaService.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace AquaSolution.Server.Controllers.UserManagements
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController :ControllerBase
    {
        [HttpGet("mock-users")]
        public IActionResult GetMockUsers()
        {
            return Ok(TokenServiceMock.GetMockUsers());
        }
    }
}
