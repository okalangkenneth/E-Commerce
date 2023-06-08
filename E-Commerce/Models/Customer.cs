using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace E_Commerce.Models
{
    
    public class Customer
    {
        
        public int Id { get; set; }

       
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
