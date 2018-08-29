using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductScanner.Database.Entities;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Interfaces;
using ProductScanner.Services.Interfaces;
using ProductScanner.ViewModels.Photo;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductScanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotoController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhotoController(
            IMapper mapper,
            IEventBus eventBus,
            UserManager<ApplicationUser> userManager,
            IPhotoService photoService)
        {
            _mapper = mapper;
            _eventBus = eventBus;
            _userManager = userManager;
            _photoService = photoService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PhotoDetailsViewModel>>> Get(
            [FromQuery(Name = "page")] int page, 
            [FromQuery(Name = "limit")] int limit)
        {
            var data = await _photoService.Get(page, limit);
            return _mapper.Map<IEnumerable<PhotoDetailsViewModel>>(data).ToArray();
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

            var integrationEvent = _mapper.Map<ImageClasificationEvent>(result);
            _eventBus.Publish(integrationEvent);

            return Ok(new { id = result.Id });
        }

        [HttpGet("{photoId}")]
        public async Task<ActionResult<PhotoDetailsViewModel>> Details(int photoId)
        {
            var data = await _photoService.Get(photoId);
            if (data == null)
            {
                return NotFound();
            }
            return _mapper.Map<PhotoDetailsViewModel>(data);
        }

        [HttpDelete("{photoId}")]
        public IActionResult Delete(int photoId)
        {
            _photoService.Delete(photoId);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("{fileId}/Image")]
        public async Task<IActionResult> GetImage(int fileId)
        {
            string path = await _photoService.GetPathById(fileId);
            if (string.IsNullOrEmpty(path))
            {
                return NotFound();
            }
            return new FileStreamResult(new FileStream(path, FileMode.Open), "image/jpeg");
        }

        [AllowAnonymous]
        [HttpGet("{fileId}/Analyse")]
        public async Task<IActionResult> GetAnalysed(int fileId)
        {
            var photo = await _photoService.Get(fileId);
            var path = photo.AnalysedFilePath;
            if (string.IsNullOrEmpty(path))
            {
                return NotFound();
            }
            return new FileStreamResult(new FileStream(path, FileMode.Open), "image/jpeg");
        }
    }
}