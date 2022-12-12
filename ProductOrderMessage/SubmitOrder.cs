using SharedMessages;
namespace SharedMessages;




public interface SubmitOrder
{
    public Guid OrderId { get; set; }
    public List<ProductQuantities> ProductQuantities { get; set; }

    public DateTime OrderDate { get; set; }
}