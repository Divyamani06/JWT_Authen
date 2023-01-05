using JWT_Authen.Model;
using Microsoft.EntityFrameworkCore;

namespace JWT_Authen.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserCrud> UserCruds { get; set; }
       
    }
}
