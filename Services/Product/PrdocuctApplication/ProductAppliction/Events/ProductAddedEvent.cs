using MediatR;

namespace ProductAppliction.Events
{
    public class ProductAddedEvent : INotification
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
