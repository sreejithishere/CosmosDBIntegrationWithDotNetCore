using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
  public  interface IRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(string query);

        Task<Product> GetProductByIdAsync(string id);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task AddProductAsync(Product item);

        Task UpdateProductAsync(string id, Product item);

        Task DeleteProductAsync(string id);
    }
}
