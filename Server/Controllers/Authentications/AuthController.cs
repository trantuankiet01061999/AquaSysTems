
using Microsoft.AspNetCore.Mvc;
using AquaService.Server.Services;
using AquaService.Shared.AuthModels;
namespace AquaService.Server.Controllers.Authentications
{


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenServiceMock _tokenService;
        public AuthController(TokenServiceMock tokenService) => _tokenService = tokenService;

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var token = _tokenService.GenerateToken(request.UserName, request.Password);
            if (token == null) return Unauthorized();

            return Ok(new LoginResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2)
            });
        }
    }

}
