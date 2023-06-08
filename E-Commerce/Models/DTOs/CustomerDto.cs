using System.Collections.Generic;
using System.Runtime.Serialization;

namespace E_Commerce.Models.DTOs
{
    [DataContract]
    public class CustomerDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public ICollection<OrderDto> Orders { get; set; }

        [DataMember]
        public ICollection<ShoppingCartItemDto> ShoppingCartItems { get; set; }
    }
}
