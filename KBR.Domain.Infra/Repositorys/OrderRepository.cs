using KBR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace KBR.Domain.Infra.Repositorys
{
    public class OrderRepository
    {
        private readonly dbContext db;
        private readonly RestClient client = new RestClient("https://localhost:7265");

        public OrderRepository(dbContext _db)
        {
            db = _db;
        }

        public async ValueTask<Order> GetLastOrCreate() 
        {
            Order order = db.orders.FirstOrDefault(i => i.statusId == db.orderStatus.First(i => i.order == 1).Id);
            if (order != null)
            {
                order.items = db.orderItems.ToList();
            }
            else
            {
                order = new Order();
                order.statusId = db.orderStatus.First(i => i.order == 1).Id;
                db.orders.Add(order);
                db.SaveChanges();
            }
            return order;
        }

        public List<Order> GetAll()
        {
            return db.orders.Where(i => i.items.Count > 0).ToList();
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
            order = db.orders.First(i => i.Id == order.Id);
            order.items = db.orderItems.ToList();
            if (order.items.Count == 0) return null;
            order.statusId = (await db.orderStatus.FirstAsync(i => i.order == 2)).Id;
            await db.SaveChangesAsync();
            return order;
        }

        public async void Pay(Order order)
        {
            order = db.orders.First(i => i.Id == order.Id);
            order.items = db.orderItems.ToList();
            if (order.items.Count != 0 && order.statusId == db.orderStatus.First(i => i.order == 2).Id) 
            {
                order.statusId = db.orderStatus.First(i => i.order == 3).Id;
                db.SaveChanges();
                var request = new RestRequest("/payments");
                request.AddJsonBody(JsonConvert.SerializeObject(order));
                request.AddHeader("Accept", "*/*");
                client.ExecutePostAsync(request);
            }
        }

        public async ValueTask<Order> PaymentHook(Payment payment)
        {
            if (payment.status != 1) return null;
            Order order = await db.orders.FirstAsync(i => i.Id == payment.orderId);
            order.statusId = db.orderStatus.First(i => i.order == 4).Id;
            await db.SaveChangesAsync();
            return order;
        }
    }
}
