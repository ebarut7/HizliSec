using HizliSec.Business.Abstract;
using HizliSec.Entities.Dtos.LoginDtos;
using HizliSec.Entities.Dtos.SellerDtos;
using Microsoft.AspNetCore.Mvc;

namespace HizliSec.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Account(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            Microsoft.AspNetCore.Identity.SignInResult response = await _authService.LoginAsync(loginDto);
            if (response.Succeeded) 
            {
                //var user = await _authService.GetUserAsync(loginDto.UserName);
                //var roles = await _authService.GetRolesAsync(user);
                //switch (roles.Contains("seller"))
                //{
                //    case true: return RedirectToAction("index","home"); 
                //    case false: return RedirectToAction("index", "product");
                //}
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SellerAddDto sellerAdd)
        {
            bool response = (await _authService.SellerRegisterAsync(sellerAdd)).Succeeded;
            return RedirectToAction("Account",new {message = response ? "Kayit işlemi başarılı.":"Bir hata oluştu."});
        }


    }
}
