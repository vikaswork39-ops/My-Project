using EmployeeAPIWithJWT_123.Identity;
using EmployeeAPIWithJWT_123.Models.ViewModels;

namespace EmployeeAPIWithJWT_123.ServiceContract
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(LoginVM loginVM);
    }
}
