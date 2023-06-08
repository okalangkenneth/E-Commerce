using E_Commerce.Models.DTOs;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace E_Commerce.Models.Dtos
{
    [DataContract]
    public class ProductDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public CategoryDto Category { get; set; }

        [DataMember]
        public ICollection<ProductImageDto> Images { get; set; }
    }


}
