using Microsoft.AspNetCore.Identity;

namespace WebXeHoi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {  get; set; }
    }
}
