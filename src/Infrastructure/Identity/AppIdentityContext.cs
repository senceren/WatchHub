using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class AppIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options)
            : base(options)
        {
        }
    }
}