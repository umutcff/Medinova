using Microsoft.AspNetCore.Identity;

namespace Medinova.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
