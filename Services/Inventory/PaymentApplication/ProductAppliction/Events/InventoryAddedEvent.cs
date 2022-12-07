using MediatR;

namespace InventoryAppliction.Events
{
    public class InventoryAddedEvent : INotification
    {
        public int ProductId { get; set; }
        public int Qauntity { get; set; }
    }
}
