using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithTypeAndBrandSpecifications : BaseSpecifications<Product>
    {
        public ProductsWithTypeAndBrandSpecifications(ProductSpecParams productSpec) 
            : base( product => 
                (string.IsNullOrEmpty(productSpec.Search) || product.Name.ToLower().Contains(productSpec.Search)) &&
                (!productSpec.BrandId.HasValue || product.ProductBrandId == productSpec.BrandId) &&
                (!productSpec.TypeId.HasValue || product.ProductTypeId == productSpec.TypeId) 
            )
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            AddOrderBy(product => product.Name);
            ApplayPaging(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);

            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(product => product.Price);
                        break;

                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }
        }

        public ProductsWithTypeAndBrandSpecifications(int id) 
            : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }


    }
}
