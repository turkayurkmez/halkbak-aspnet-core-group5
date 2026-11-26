using ECommerce.API.Options;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ShopOptions shopOptions;
        private readonly IProductService productService;
        public ProductsController(IOptions<ShopOptions> options, IProductService productService)
        {
            shopOptions = options.Value;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
           
            return Ok(await productService.GetProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
          
            return Ok(await productService.GetProductAsync(id));
        }

        [HttpGet("info")]
        public IActionResult GetShopInfo()
        {
            return Ok(shopOptions);
        }
    }
}
