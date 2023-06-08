using E_Commerce.Models;
using System.Runtime.Serialization;

[DataContract]
public class OrderDetailDto
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public int OrderId { get; set; }

    [DataMember]
    public Order Order { get; set; }

    [DataMember]
    public int ProductId { get; set; }

    [DataMember]
    public Product Product { get; set; }

    [DataMember]
    public int Quantity { get; set; }
}