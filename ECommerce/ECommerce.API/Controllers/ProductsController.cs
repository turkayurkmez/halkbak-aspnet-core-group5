using ECommerce.API.Options;
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
        public IActionResult GetProducts()
        {
            return Ok(new {message="Burada ürünler döndürülecek!"} );
        }
        
        [HttpGet("info")]
        public IActionResult GetShopInfo()
        {       
            return Ok(shopOptions);
        }
    }
}
