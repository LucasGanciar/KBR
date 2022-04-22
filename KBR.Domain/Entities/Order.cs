using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBR.Domain.Entities
{
    public class Order : Entity
    {
        public List<OrderItem>? items { get; set; }
        public double value { get; set; }

        [ForeignKey("OrderStatus")]
        public Guid statusId { get; set; }
    }
}
