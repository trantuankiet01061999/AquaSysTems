using AquaSolution.Server.Services.Administration.SystemLock;
using AquaSolution.Server.SignalR;
using AquaSolution.Shared.Administration.SystemLock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AquaSolution.Server.Controllers.Administration.SystemLocks
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemLockController : ControllerBase
    {
        private readonly ISystemLockService _SystemLockService;
        private readonly IHubContext<SignalrHub> _hubContext;

        public SystemLockController(ISystemLockService SystemLockService, IHubContext<SignalrHub> hubContext)
        {
            _SystemLockService = SystemLockService;
            _hubContext = hubContext;
        }

        [HttpGet("system-locks")]
        public async Task<IActionResult> GetSystemLockByURL()
        {
            var result = await _SystemLockService.LoadDataAsync();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatedAsync([FromBody] SystemLockDto systemLockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _SystemLockService.CreatedAsync(systemLockDto);
            if (result)
                return Ok(new { success = true, message = "SystemLock created successfully." });

            return StatusCode(500, new { success = false, message = "Failed to create SystemLock." });
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] SystemLockDto systemLockDto)
        {
            var result = await _SystemLockService.UpdateStatus(systemLockDto.Id, systemLockDto.IsLocket);
            await _hubContext.Clients.All.SendAsync("IsLockSystem", systemLockDto.PageId);
            return Ok(result);
        }
        [HttpGet("check-lock/{pageId}")]
        public async Task<bool> CheckIsLock(Guid pageId)
        {
            var systemLock = await _SystemLockService.CheckLock(pageId);
                return systemLock;
        }
    }

}
