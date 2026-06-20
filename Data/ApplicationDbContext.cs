using Microsoft.EntityFrameworkCore;
using JobBoard.Web.Models;


namespace JobBoard.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}