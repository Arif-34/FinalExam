using Indingo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Indingo.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) 
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
