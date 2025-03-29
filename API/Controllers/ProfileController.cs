using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories.IRepositories;
using System.Security.Cryptography;
using System.Text;
using API.DTO.Request;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _profileRepository.GetProfileAsync(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _profileRepository.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}/readings")]
        public async Task<IActionResult> GetReadings(int id)
        {
            var readings = await _profileRepository.GetAccountReadingsAsync(id);
            return Ok(readings);
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile([FromForm] ProfileRequest request)
        {
            var response = await _profileRepository.UpdateProfileAsync(request);
            if (!string.IsNullOrEmpty(response.Message) && response.Message.Contains("thành công"))
                return Ok(response);
            return BadRequest(response);
        }
    }
}