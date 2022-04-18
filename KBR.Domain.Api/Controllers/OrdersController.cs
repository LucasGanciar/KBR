using KBR.Domain.Entities;
using KBR.Domain.Infra.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace KBR.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrdersController : ControllerBase
    {
        internal readonly OrderRepository orderRepository;
        public OrdersController(OrderRepository repository)
        {
            orderRepository = repository;
        }

        [HttpGet]
        public async ValueTask<ActionResult> GetLastOrCreate()
        {
            try
            {
                Order order = await orderRepository.GetLastOrCreate();
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("api/[Controller]/all")]
        public async ValueTask<ActionResult> GetAll()
        {
            try
            {
                List<Order> orders = await orderRepository.GetAll();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async ValueTask<ActionResult> AddItem([FromBody]OrderItem item)
        {
            try
            {
                Order order = await orderRepository.AddItem(item);
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult> UpdateItem(OrderItem item)
        {
            try
            {
                OrderItem orderItem = await orderRepository.UpdateItem(item);
                return Ok(orderItem);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> RemoveItem(Guid id)
        {
            try
            {
                OrderItem item = await orderRepository.RemoveItem(id);
                return Ok(item);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
