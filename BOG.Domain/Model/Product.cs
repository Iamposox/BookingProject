using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOG.Domain.Model
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
