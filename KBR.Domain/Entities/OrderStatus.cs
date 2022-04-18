using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBR.Domain.Entities
{
    public class OrderStatus : Entity
    {
        public int order { get; set; }
        public string status { get; set; }
    }
}
