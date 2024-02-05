using HizliSec.Business.Abstract;
using HizliSec.Entities.Concrete;
using HizliSec.Entities.Dtos.CustomerDtos;
using HizliSec.Entities.Dtos.LoginDtos;
using HizliSec.Entities.Dtos.SellerDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace HizliSec.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public AuthManager(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role)
        {

            AppRole appRole = _roleManager.Roles.FirstOrDefault(x => x.Name == role);
            if (appRole is null)
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
            return await _userManager.AddToRoleAsync(appUser, role);
        }

        public async Task<IdentityResult> CustomerRegisterAsync(CustomerAddDto customerDto)
        {
            AppUser appUser = new AppUser()
            {
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                UserName = customerDto.UserName
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, customerDto.Password);
            Customer customer = new()
            {
                Id = appUser.Id,
                Address = customerDto.Address,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName
            };
            await _unitOfWork.CustomerDal.AddAsync(customer);
            await _unitOfWork.SaveAsync();

            if (result.Succeeded)
            {
                await AddToRoleAsync(appUser, "customer");
                await _unitOfWork.SaveAsync();
            }
            return result;
        }
        public async Task<IdentityResult> SellerRegisterAsync(SellerAddDto sellerDto)
        {
            AppUser appUser = new AppUser()
            {
                Email = sellerDto.Email,
                PhoneNumber = sellerDto.PhoneNumber,
                UserName = sellerDto.UserName
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, sellerDto.Password);
            Seller seller = new Seller()
            {
                Id = appUser.Id,
                Address = sellerDto.Address,
                CompanyName = sellerDto.CompanyName,
                FirstName = sellerDto.FirstName,
                LastName = sellerDto.LastName
            };
            await _unitOfWork.SellerDal.AddAsync(seller);
            await _unitOfWork.SaveAsync();

            if (result.Succeeded)
            {
                await AddToRoleAsync(appUser, "seller");
                await _unitOfWork.SaveAsync();
            }
            return result;
        }
        public async Task<SignInResult> LoginAsync(LoginDto loginDto)
        {
            AppUser user;
            if (loginDto.UserName.Contains("@"))
            {
                user = _userManager.Users.FirstOrDefault(x => x.Email == loginDto.UserName);
            }
            else
            {
                user = _userManager.Users.FirstOrDefault(x => x.UserName == loginDto.UserName);
            }
            return user is not null ? await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false) : null;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> PasswordResetAsync(string userName, string newPassword)
        {
            string token = null;
            AppUser user = await GetUserAsync(userName);
            IdentityResult result = await _userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                token = await _userManager.GeneratePasswordResetTokenAsync(user);
            }
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<IdentityResult> UpdatePasswordAsync(string userName, string currentPassword, string newPassword)
        {
            AppUser user = await GetUserAsync(userName);
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<AppUser> GetUserAsync(string userName)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => !userName.Contains("@") ? x.UserName == userName : x.Email == userName);
        }

        public async Task<IdentityResult> RemoveUserAsync(string userName)
        {
            IdentityResult result = null;
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    AppUser user = await GetUserAsync(userName);
                    // gelen role gore seller veya customer silinecek.
                    Seller seller = await _unitOfWork.SellerDal.GetAsync(x => x.Id == user.Id);
                    result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        await _unitOfWork.SellerDal.DeleteAsync(seller);
                        await _unitOfWork.SaveAsync();
                    }
                    ts.Complete();
                }
                catch (Exception)
                {
                    ts.Dispose();
                }
            }
            return result;
        }

        public async Task<List<string>> GetRolesAsync(AppUser user)
        {
            List<string> roles = (await _userManager.GetRolesAsync(user)).ToList();
            return roles;
        }
        public async Task<string> CreateTokenAsync(LoginDto loginDto)
        {
            string token = null;
            AppUser user = await _userManager.FindByEmailAsync(loginDto.UserName);
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, user.LockoutEnabled);
            if (user is not null && result.Succeeded)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("UserName",user.UserName),
                    new Claim("Email",user.Email)
                };
                List<string> roles =  GetRolesAsync(user).Result;
                foreach (string item in roles)
                {
                    claims.Add(new Claim("Role",item));
                }
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
                var key = Encoding.UTF8.GetBytes("keykullaniyorumburadaasdasdfhashgdvasd");
                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
                {
                    //Sağlayıcı
                    Audience = "localhost",
                    //Kullanıcı 
                    Issuer = "localhost",
                    // Claimsler 
                    Subject = claimsIdentity,
                    // Geçerlilik süresi
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                };
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SecurityToken jwtToken = handler.CreateToken(descriptor);

                token = handler.WriteToken(jwtToken);
            }
            return token;
        }
    }
}
