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

        [HttpGet]
        [Route("api/[Controller]/All")]
        public async ValueTask<ActionResult> GetAll()
        {
            try
            {
                List<Order> orders = orderRepository.GetAll();
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

        [HttpPost]
        [Route("api/[controller]/checkout")]
        public async ValueTask<ActionResult> Checkout([FromBody] Order order)
        {
            try
            {
                Order checkout = await orderRepository.Checkout(order);
                return Ok(checkout);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("api/[controller]/pay")]
        public async ValueTask<ActionResult> Pay([FromBody]Order order)
        {
            try
            {
                orderRepository.Pay(order);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("api/[controller]/payhook")]
        public async ValueTask<ActionResult> PayHook([FromBody]Payment payment)
        {
            try
            {
                Order order = await orderRepository.PaymentHook(payment);
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
