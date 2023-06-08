using System.Runtime.Serialization;

namespace E_Commerce.Models.DTOs
{

    [DataContract]
    public class CategoryDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

}
