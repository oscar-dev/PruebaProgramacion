using ProyectoPrueba.Factories;
using ProyectoPrueba.Interfaces;
using ProyectoPrueba.Models;

namespace ProyectoPrueba.Services
{
    public class ProductRepository
        : IProductRepository, IDisposable
    {
        private ContextFactory _contextFactory;
        private bool disposed = false;

        public ProductRepository(ContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        protected virtual void Dispose(bool disposing)
        {
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IList<Product> getAll(string slug)
        {
            ProductContext productContext = this._contextFactory.GetContext(slug);

            if (productContext == null) throw new Exception("Contexto no encontrado");

            using( var context = productContext )
            {
                var products = from p in context.Products
                               select new Product
                               {
                                   ProductId = p.ProductId,
                                   Name = p.Name
                               };

                return products.ToList();
            }
        }

        public int insert(string slug, Product product)
        {
            if (product.Name.Trim().Length <= 0) throw new Exception("No se ingreso el nombre para el producto");

            ProductContext productContext = this._contextFactory.GetContext(slug);

            if (productContext == null) throw new Exception("Contexto no encontrado");

            productContext.Products.Add(product);

            productContext.SaveChanges();

            return product.ProductId;
        }
        public int update(string slug, Product product)
        {
            ProductContext productContext = this._contextFactory.GetContext(slug);

            if (productContext == null) throw new Exception("Contexto no encontrado");

            var productDB = productContext.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();

            if (productDB == null) throw new Exception("Producto no existente");

            productDB.Name = product.Name;

            productContext.SaveChanges();

            return product.ProductId;
        }
        public void delete(string slug, int productId)
        {
            ProductContext productContext = this._contextFactory.GetContext(slug);

            if (productContext == null) throw new Exception("Contexto no encontrado");

            var productDB = productContext.Products.Where(p => p.ProductId == productId).FirstOrDefault();

            if (productDB == null) throw new Exception("Producto no existente");

            productContext.Products.Remove(productDB);

            productContext.SaveChanges();
        }
    }
}
