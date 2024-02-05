
using HizliSec.Entities.Concrete;
using HizliSec.Entities.Dtos.CustomerDtos;
using HizliSec.Entities.Dtos.LoginDtos;
using HizliSec.Entities.Dtos.SellerDtos;
using Microsoft.AspNetCore.Identity;

namespace HizliSec.Business.Abstract
{
    public interface IAuthService
    {
        Task<List<string>> GetRolesAsync(AppUser user);
        Task<IdentityResult> RemoveUserAsync(string userName);
        Task<AppUser> GetUserAsync(string userName);
        Task<IdentityResult> PasswordResetAsync(string userName, string newPassword);
        Task<IdentityResult> UpdatePasswordAsync(string userName, string currentPassword, string newPassword);
        Task<SignInResult> LoginAsync(LoginDto loginDto);
        Task<IdentityResult> SellerRegisterAsync(SellerAddDto sellerDto);
        Task<IdentityResult> CustomerRegisterAsync(CustomerAddDto customerDto);
        Task SignOutAsync();
        Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role);
        Task<string> CreateTokenAsync(LoginDto loginDto);
    }
}
