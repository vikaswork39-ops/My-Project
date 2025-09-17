using EmployeeAPIWithJWT_123.Identity;
using EmployeeAPIWithJWT_123.Models.ViewModels;
using EmployeeAPIWithJWT_123.ServiceContract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeAPIWithJWT_123.Services
{
    public class UserServices : IUserService
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSignInManager _applicationSignInManager;
        private readonly AppSettings _appSettings;
        public UserServices(ApplicationUserManager applicationUserManager, ApplicationSignInManager applicationSignInManager,IOptions<AppSettings> appsettings)
        {
            _applicationSignInManager = applicationSignInManager;
            _applicationUserManager = applicationUserManager;
            _appSettings = appsettings.Value;
        }
        public async Task<ApplicationUser> Authenticate(LoginVM loginVM)
        {
            var result=await _applicationSignInManager.PasswordSignInAsync(loginVM.Name, loginVM.Password,false,false);
            if (result.Succeeded)
            {
                var applicationUser = await _applicationUserManager.FindByNameAsync(loginVM.Name);
                applicationUser.PasswordHash = "";
                //JWT Token
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Admin))
                    applicationUser.Role = SD.Role_Admin;
                if(await _applicationUserManager.IsInRoleAsync(applicationUser,SD.Role_Employee))
                    applicationUser.Role= SD.Role_Employee;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name,applicationUser.Id.ToString()),
                    new Claim(ClaimTypes.Email,applicationUser.Email),
                    new Claim(ClaimTypes.Role,applicationUser.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                applicationUser.Token = tokenHandler.WriteToken(token);



                //****
                return applicationUser;
            }
            return null;
             
        }
    }
}
