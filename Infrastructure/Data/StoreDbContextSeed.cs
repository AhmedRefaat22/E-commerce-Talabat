using Core.Entities;
using Core.Entities.OrderAggreagate;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreDbContextSeed
    {
        public async static Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
			try
			{
				if(context.ProductBrands != null && !context.ProductBrands.Any())
				{
					var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

					foreach( var brand in brands) 
						context.ProductBrands.Add(brand);

					await context.SaveChangesAsync();
				}

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var type in types)
                        context.ProductTypes.Add(type);

                    await context.SaveChangesAsync();
                }

                if (context.Products != null && !context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var product in products)
                        context.Products.Add(product);

                    await context.SaveChangesAsync();
                }

                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");

                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    foreach (var method in deliveryMethods)
                        context.DeliveryMethods.Add(method);

                    await context.SaveChangesAsync();
                }
            }
			catch (Exception ex)
			{
                var logger = loggerFactory.CreateLogger<StoreDbContextSeed>();

                logger.LogError(ex.Message);
			}
        } 
    }
}
