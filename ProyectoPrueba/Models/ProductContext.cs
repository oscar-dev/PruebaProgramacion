using Microsoft.EntityFrameworkCore;

namespace ProyectoPrueba.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {
        }
        public ProductContext(DbContextOptions<ProductContext> options)
                                                        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
