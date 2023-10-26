using ProyectoPrueba.Models;

namespace ProyectoPrueba.Factories
{
    public class ContextFactory
    {
        private readonly IDictionary<string, ProductContext> _context;

        public ContextFactory(IDictionary<string, ProductContext> context)
        {
            _context = context;
        }

        public ProductContext GetContext(string slug)
        {
            return _context[slug];
        }
    }
}
