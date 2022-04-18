using KBR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace KBR.Domain.Infra.Repositorys
{
    public class OrderRepository
    {
        private readonly dbContext db;
        private readonly RestClient client = new RestClient("");

        public OrderRepository(dbContext _db)
        {
            db = _db;
        }

        public async ValueTask<Order> GetLastOrCreate() 
        {
            Order order = new Order();
            if (db.orders.Any(i => i.paid == false))
            {
                order = db.orders.First(i => i.paid == false);
                order.items = db.orderItems.ToList();
            } 
            else
            {
                order.status = await db.orderStatus.FirstAsync(i => i.order == 1);
                db.orders.Add(order);
                await db.SaveChangesAsync();
            }
            return order;
        }

        public async ValueTask<List<Order>> GetAll()
        {
            return db.orders.ToList();
        }

        public async ValueTask<Order> AddItem(OrderItem item)
        {
            Order order = await GetLastOrCreate();
            if (order.items == null) order.items = new List<OrderItem>();
            order.items.Add(item);
            order.value = UpdateValue(order);
            db.orderItems.Add(item);
            db.orders.Update(order);
            await db.SaveChangesAsync();
            return order;
        }

        public async ValueTask<OrderItem> UpdateItem(OrderItem item)
        {
            db.orderItems.Update(item);
            await db.SaveChangesAsync();
            return item;
        }

        public async ValueTask<OrderItem> RemoveItem(Guid id)
        {
            OrderItem item = await db.orderItems.FirstAsync(i => i.Id == id);
            db.orderItems.Remove(item);
            await db.SaveChangesAsync();
            return item;
        }

        public double UpdateValue(Order order)
        {
            if (order.items == null) order.items = new List<OrderItem>();
            return order.items.Sum(i => i.price * i.qtd);
        }

        public async ValueTask<Order> Checkout(Order order)
        {
            if(order.items.Count == 0) return null;
            order.status = await db.orderStatus.FirstAsync(i => i.order == 2);
            await db.SaveChangesAsync();
            return order;
        }

        public async void Pay(Order order)
        {
            if (order.items.Count != 0) 
            {
                order.status = await db.orderStatus.FirstAsync(i => i.order == 3);
                await db.SaveChangesAsync();
                var request = new RestRequest("/payment", Method.Post);
                request.AddJsonBody(JsonConvert.SerializeObject(order));
                client.ExecuteAsync(request);
            }
        }

        public async ValueTask<Order> PaymentHook(Payment payment)
        {
            if (payment.status != "Paid") return null;
            payment.order.status = await db.orderStatus.FirstAsync(i => i.order == 4);
            await db.SaveChangesAsync();
            return payment.order;
        }
    }
}
