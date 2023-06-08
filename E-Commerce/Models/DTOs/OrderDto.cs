using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace E_Commerce.Models.DTOs
{
    [DataContract]

    public class OrderDto
    {

        [DataMember]
        public int Id { get; set; }


        [DataMember]
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [DataMember]
        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        [DataMember]
        [Required]
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }
    }

}
