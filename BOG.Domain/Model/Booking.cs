using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOG.Domain.Model
{
    public class Booking
    {
        public int ID { get; set; }
        public bool Statuc { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
