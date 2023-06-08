using System.Collections.Generic;
using System.Runtime.Serialization;

namespace E_Commerce.Models
{
    
    public class Product
    {
       
        public int Id { get; set; }

       
        public string Name { get; set; }

        
        public string Description { get; set; }

        
        public decimal Price { get; set; }

        
        public int CategoryId { get; set; }

        
        public Category Category { get; set; }

       
        public ICollection<ProductImage> Images { get; set; }

        
        public ICollection<OrderDetail> OrderDetails { get; set; }

       
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }

}
