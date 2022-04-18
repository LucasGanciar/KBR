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

        public async ValueTask<Payment> Pay(Order order)
        {
            Payment payment = new Payment();
            payment.order = order;
            payment.status = "Pago";
            db.payments.Add(payment);
            await db.SaveChangesAsync();
            Thread.Sleep(500);
            return payment;
        }
    }
}
