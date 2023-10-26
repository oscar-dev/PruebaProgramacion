using Microsoft.EntityFrameworkCore;

namespace ProyectoPrueba.Models
{
    public class BaseContext : DbContext
    {
        public BaseContext()
        {
        }
        public BaseContext(DbContextOptions<BaseContext> options)
                                                        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }

    }

}
