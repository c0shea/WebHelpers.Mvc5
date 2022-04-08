using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.Demo.Models
{
    public class Product
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
