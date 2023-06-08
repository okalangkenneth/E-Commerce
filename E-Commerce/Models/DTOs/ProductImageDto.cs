using System.Runtime.Serialization;

namespace E_Commerce.Models.DTOs
{
    [DataContract]
    public class ProductImageDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string AltText { get; set; }
    }

}
