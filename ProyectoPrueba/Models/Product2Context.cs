using Microsoft.EntityFrameworkCore;

namespace ProyectoPrueba.Models
{
    public class Product2Context : ProductContext
    {
        public Product2Context(DbContextOptions<ProductContext> options)
                                                : base(options)
        {
        }
    }
}
