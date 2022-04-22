using KBR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBR.Domain.Infra.Repositorys
{
    public class PaymentRepository
    {
        private readonly dbContext db;

        public PaymentRepository(dbContext _db)
        {
            db = _db;
        }

        public async ValueTask Pay(Order order)
        {
            Payment payment = new Payment();
            payment.orderId = order.Id;
            payment.status = 1;
            db.payments.Add(payment);
            db.SaveChanges();
            Thread.Sleep(500);
        }
    }
}
