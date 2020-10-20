using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOG.Domain.Model
{
    public class Reserved
    {
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public DateTime TimeOrder { get; set; }
        public Booking Booking { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int AvailableProductID { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
