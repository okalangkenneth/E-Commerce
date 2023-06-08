using System.Runtime.Serialization;

namespace E_Commerce.Models.DTOs
{
    [DataContract]
    public class ShoppingCartItemDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public Customer Customer { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public Product Product { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}
