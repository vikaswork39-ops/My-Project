using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeeAPIWithJWT_123.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context)
        {

        }
    }
}
