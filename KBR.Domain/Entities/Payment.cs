using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBR.Domain.Entities
{
    public class Payment : Entity
    {
        public Order order { get; set; }
        public Guid code { get; set; }
        public double value { get; set; }
        public double total { get; set; }
        public DateTime confirmationDate { get; set; }
        public string status { get; set; }
        public Address address { get; set; }
    }
}
