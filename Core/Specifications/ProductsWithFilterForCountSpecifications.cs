using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFilterForCountSpecifications : BaseSpecifications<Product>
    {
        public ProductsWithFilterForCountSpecifications(ProductSpecParams productSpec)
            : base(product =>
                (string.IsNullOrEmpty(productSpec.Search) || product.Name.ToLower().Contains(productSpec.Search)) &&
                (!productSpec.BrandId.HasValue || product.ProductBrandId == productSpec.BrandId) &&
                (!productSpec.TypeId.HasValue || product.ProductTypeId == productSpec.TypeId)
            )
        {

        }
    }
}
