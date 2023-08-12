using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductByIdAsync(int? id)
            => await _context.Products
                     .Include(x => x.ProductType)
                     .Include(x => x.ProductBrand)
                     .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
            => await _context.ProductBrands.ToListAsync();

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
            => await _context.Products
                     .Include(x => x.ProductType)
                     .Include(x => x.ProductBrand)
                     .ToListAsync();

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
            => await _context.ProductTypes.ToListAsync();
    }
}
