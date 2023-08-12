using APIDemo.Dtos;
using APIDemo.Helpers;
using APIDemo.ResponseModule;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        //private readonly IProductRepository _productRepository;

        public ProductsController(/*IProductRepository productRepository*/
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
            //_productRepository = productRepository;
        }
        
        [HttpGet("GetProduct/{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var specs = new ProductsWithTypeAndBrandSpecifications(id);

            var product = await _productRepository.GetEntityWithSpecifications(specs);

            if(product is null)
                return NotFound(new ApiResponse(404));

            var mappedProduct = _mapper.Map<ProductDto>(product);

            return Ok(mappedProduct);

        }

        [HttpGet("GetProducts")]
        [Cached(100)]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productSpec)
        {
            var specs = new ProductsWithTypeAndBrandSpecifications(productSpec);

            var countSpec = new ProductsWithFilterForCountSpecifications(productSpec);

            var products = await _productRepository.ListAsync(specs);

            var totalItems = await _productRepository.CountAsync(countSpec);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            var paginatedData = new Pagination<ProductDto>(productSpec.PageSize, totalItems, productSpec.PageIndex, mappedProducts);

            return Ok(paginatedData);
        }    

        [HttpGet("GetProductBrands")]
        [Cached(10)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
            => Ok(await _productBrandRepository.ListAllAsync());

        [HttpGet("GetProductTypes")]
        [Cached(10)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
            => Ok(await _productTypeRepository.ListAllAsync());
    }
}
