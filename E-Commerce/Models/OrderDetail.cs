using System.ComponentModel;
using System.Runtime.Serialization;

namespace E_Commerce.Models
{
   
    public class OrderDetail
    {
        
        public int Id { get; set; }

        
        public int OrderId { get; set; }

        public Order Order { get; set; }

        
        public int ProductId { get; set; }

        public Product Product { get; set; }

        
        public int Quantity { get; set; }
    }
}
