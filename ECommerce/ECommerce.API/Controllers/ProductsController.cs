using ECommerce.API.Options;
using ECommerce.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ShopOptions shopOptions;
        public ProductsController(IOptions<ShopOptions> options)
        {
            shopOptions = options.Value;
        }

        [HttpGet]        
        public async Task<IActionResult> GetProducts()
        {
            var productService = new ProductService();
            return Ok(await productService.GetProductsAsync());
        }
        
        [HttpGet("info")]
        public IActionResult GetShopInfo()
        {       
            return Ok(shopOptions);
        }
    }
}
