using ProyectoPrueba.Models;

namespace ProyectoPrueba.Interfaces
{
    public interface IProductRepository
    {
        IList<Product> getAll(string slug);
        int insert(string slug, Product product);
        int update(string slug, Product product);
        void delete(string slug, int productId);
    }
}
