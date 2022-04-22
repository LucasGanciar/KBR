using KBR.Domain.Entities;

namespace KBR.Domain.Infra.Repositorys
{
    public class ProductRepository
    {
        internal readonly dbContext db;

        public ProductRepository(dbContext _db)
        {
            db = _db;
        }

        public async ValueTask<Product> Add(Product product)
        {
            db.Add(product);
            await db.SaveChangesAsync();
            return product;
        }

        public async ValueTask<Product> Get(Guid id) 
        {
            Product product = db.products.FirstOrDefault(i => i.Id == id);
            return product;
        }

        public async ValueTask<List<Product>> GetAll()
        {
            List<Product> products = db.products.ToList();
            return products;
        }

        public async ValueTask<List<Product>> GetProductsOfType(Guid id)
        {
            List<Product> products = db.products.Where(i => i.type.Id == id).ToList();
            return products;
        }
    }
}
