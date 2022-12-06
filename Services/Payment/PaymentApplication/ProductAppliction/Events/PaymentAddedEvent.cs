using MediatR;

namespace PaymentAppliction.Events
{
    public class PaymentAddedEvent : INotification
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
