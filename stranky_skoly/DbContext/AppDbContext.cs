using Microsoft.EntityFrameworkCore;
using stranky_skoly.Models;

namespace stranky_skoly.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

