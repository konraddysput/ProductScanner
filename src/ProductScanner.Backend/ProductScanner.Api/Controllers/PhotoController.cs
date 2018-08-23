using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductScanner.Database.Entities;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Interfaces;
using ProductScanner.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace ProductScanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotoController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IEventBus _eventBus;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhotoController(
            IEventBus eventBus,
            UserManager<ApplicationUser> userManager,
            IPhotoService photoService)
        {
            _eventBus = eventBus;
            _userManager = userManager;
            _photoService = photoService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }
            var userId = int.Parse(_userManager.GetUserId(User));
            var result = await _photoService.Create(file, userId);
            await _photoService.SaveChanges();

            var integrationEvent = new ImageClasificationIntegrationEvent()
            {
                Id = result.Id,
                Path = result.Path
            };
            _eventBus.Publish(integrationEvent);

            return Ok(new { id = result.Id });
        }

        [HttpGet("{fileId}")]
        public IActionResult Get(int fileId)
        {
            string path = _photoService.GetById(fileId);
            if (string.IsNullOrEmpty(path))
            {
                return NotFound();
            }
            return new FileStreamResult(new FileStream(path, FileMode.Open), "image/jpeg");
        }
    }
}