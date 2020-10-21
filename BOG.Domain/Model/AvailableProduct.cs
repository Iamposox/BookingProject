using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOG.Domain.Model
{
    public class AvailableProduct
    {
        public int ID { get; set; }
        [ConcurrencyCheck]
        public Product Product { get; set; }
        public int Price { get; set; }
        [ConcurrencyCheck]
        public int Amount { get; set; }
        public int ProductID { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
