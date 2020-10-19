using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOG.Domain.Model
{
    public class PaymentMethod
    {
        public int ID { get; set; }
        public string PaymentName { get; set; }
        public string DecriptionPayment { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
