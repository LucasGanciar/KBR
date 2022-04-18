using KBR.Domain.Entities;
using KBR.Domain.Infra.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace KBR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository productsRepository;

        public ProductsController(ProductRepository repository)
        {
            productsRepository = repository;
        }

        [HttpGet]
        public async ValueTask<ActionResult> GetAll()
        {
            try
            {
                List<Product> products = await productsRepository.GetAll();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("api/[controller]/type/{id}")]
        public async ValueTask<ActionResult> GetProductsOfType(Guid id)
        {
            try
            {
                List<Product> products = await productsRepository.GetProductsOfType(id);
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult> Get(Guid id)
        {
            try
            {
                Product product = await productsRepository.Get(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async ValueTask<ActionResult> Add([FromBody] Product product)
        {
            try
            {
                Product added = await productsRepository.Add(product);
                return Ok(added);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
