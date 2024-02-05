using HizliSec.Business.Abstract;
using HizliSec.Entities.Concrete;
using HizliSec.Entities.Dtos.CustomerDtos;
using HizliSec.Entities.Dtos.LoginDtos;
using HizliSec.Entities.Dtos.SellerDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HizliSec.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("Token")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
           string token= await _authService.CreateTokenAsync(loginDto);
            return string.IsNullOrEmpty(token) ? NotFound() : Ok(token);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string userName)
        {
            AppUser user = await _authService.GetUserAsync(userName);
            return user is not null ? Ok(user) : BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveUser(string userName)
        {
            IdentityResult result = await _authService.RemoveUserAsync(userName);
            return result.Succeeded ? Ok(result) : BadRequest();
        }
        [Route("Seller")]
        [HttpPost]
        public async Task<IActionResult> AddSeller(SellerAddDto sellerAddDto)
        {
            IdentityResult result = await _authService.SellerRegisterAsync(sellerAddDto);
            return Ok(result);
        }
        [Route("Customer")]
        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerAddDto customerAddDto)
        {
            IdentityResult result = await _authService.CustomerRegisterAsync(customerAddDto);
            return Ok(result);
        }
    }
}
