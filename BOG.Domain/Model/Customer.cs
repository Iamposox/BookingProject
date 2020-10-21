using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOG.Domain.Model
{
    public class Customer
    {
        public int ID { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }
        public string LastName { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int PaymentMethodID { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
