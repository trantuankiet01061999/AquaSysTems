using AquaSolution.Server.Services.ImgsService;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AquaSolution.Server.Controllers.Imgs
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImgController : ControllerBase
    {
        private readonly IImgService _imgService;

        public ImgController(IImgService imgService)
        {
            _imgService = imgService;
        }

        //[HttpGet("get-img/{workId}")]
        //public async Task<IActionResult> GetImg(string workId)
        //{
        //    var list = await _imgService.GetImagesFromCloudinary(workId);
        //    return Ok(list);
        //}
        [HttpGet("get-all-img")]
        public async Task<IActionResult> GetAllImg()
        {
            var list = await _imgService.GetAllImagesFromCloudinary();
            return Ok(list);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteImage([FromQuery] string publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId))
                return BadRequest("Missing publicId");

            try
            {
                var result = await _imgService.DeleteImageFromCloudinary(publicId);

                if (!result)
                    return NotFound("Image not found or already deleted");

                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("download")]
        public async Task<IActionResult> Download([FromQuery] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return BadRequest("Missing url");

            try
            {
                url = Uri.    UnescapeDataString(url);
                using var http = new HttpClient();
                var bytes = await http.GetByteArrayAsync(url);

                var fileName = $"{Guid.NewGuid()}.jpg";
                return File(bytes, "image/jpeg", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
