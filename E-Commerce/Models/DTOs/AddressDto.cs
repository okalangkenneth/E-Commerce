using System.Runtime.Serialization;

namespace E_Commerce.Models.DTOs
{
    [DataContract]
    public class AddressDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public Customer Customer { get; set; }
    }
}
