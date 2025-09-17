using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EmployeeAPIWithJWT_123.Identity
{
    public class ApplicationUser:IdentityUser
    {

        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
