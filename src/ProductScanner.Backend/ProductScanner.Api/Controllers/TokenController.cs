using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductScanner.Database.Entities;
using ProductScanner.Services.Interfaces;
using ProductScanner.ViewModels.Token;
using System.Linq;
using System.Threading.Tasks;

namespace ProductScanner.Api.Controllers
{
    [Route("api/auth")]
    public class TokenController : Controller
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public TokenController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            var user = _userManager.Users.FirstOrDefault(n => n.UserName == model.Login);
            return Ok(new
            {
                token = _jwtService.GenerateToken(user)
            });
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(applicationUser, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.First().Description);
        }

    }
}