using Microsoft.EntityFrameworkCore;
using WebApplicationMaxim.Models;

namespace WebApplicationMaxim.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public  DbSet<Services> Services { get; set; }
    }
}
