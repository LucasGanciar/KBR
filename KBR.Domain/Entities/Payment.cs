using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBR.Domain.Entities
{
    public class Payment : Entity
    {
        [ForeignKey("Order")]
        public Guid orderId { get; set; }
        public Guid code { get; set; }
        public double value { get; set; }
        public double total { get; set; }
        public DateTime confirmationDate { get; set; }
        public int status { get; set; }
        public Address address { get; set; }
    }
}
