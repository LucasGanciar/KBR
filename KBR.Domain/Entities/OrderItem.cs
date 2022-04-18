using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBR.Domain.Entities
{
    public class OrderItem : Entity
    {
        [ForeignKey("Product")]
        public Guid productId { get; set; }
        public double qtd { get; set; }
        public double price { get; set; }
    }
}
