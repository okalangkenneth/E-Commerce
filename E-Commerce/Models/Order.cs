using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace E_Commerce.Models
{
    
    public class Order
    {
        
        public int Id { get; set; }

       
        public DateTime OrderDate { get; set; }

        
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

       
        public int AddressId { get; set; }

        public Address ShippingAddress { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

