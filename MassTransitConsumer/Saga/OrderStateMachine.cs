using MassTransit;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitConsumer.Saga
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {

            Debug.WriteLine("Here is Order State Machine");
            Trace.WriteLine("Here is Order State Machine");

            InstanceState(x => x.CurrentState);

            Event(() => SubmitOrder, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderAccepted, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => UpdateInvetory, x => x.CorrelateById(context => context.Message.OrderId));
            
            Initially(
                 When(UpdateInvetory)
                    .Then(x => x.Saga.Quantity = 10)
                    .Then((context => Debug.WriteLine("UpdateInvetoryyyyy")))
                    .TransitionTo(InventoryUpdated),
                When(SubmitOrder)
                    .Then((context => Debug.WriteLine("updatedddd")))
                    .Then(x => x.Saga.OrderDate = x.Message.OrderDate)
                    .Publish(ctx => new InventoryQuantities { ProductQuantities = ctx.Instance.ProductQuantities = ctx.Message.ProductQuantities,OrderId = ctx.Message.OrderId})
                    .TransitionTo(Submitted),
                When(OrderAccepted)
                    .TransitionTo(Accepted));;

            During(Submitted,
                When(OrderAccepted)
                    .TransitionTo(Accepted));

            During(Accepted,
                When(SubmitOrder)
                    .Then(x => x.Saga.OrderDate = x.Message.OrderDate));
        }

        public Event<SubmitOrder> SubmitOrder { get; private set; }
        public Event<OrderAccepted> OrderAccepted { get; private set; }
        public Event<InventoryQuantities> UpdateInvetory { get; private set; }
        public Request<OrderState, ProductQuantities, InventoryQuantities> InventoryRequest { get; set; }
        public State Submitted { get; private set; }
        public State Accepted { get; private set; }
        public State InventoryUpdated { get; private set; }
    }



    public interface OrderAccepted
    {
        Guid OrderId { get; }
    }

    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public List<ProductQuantities> ProductQuantities { get; set; }
        public DateTime? OrderDate { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderStateMachineDefinition :
    SagaDefinition<OrderState>
    {
        public OrderStateMachineDefinition()
        {
            ConcurrentMessageLimit = 8;
            Endpoint(e =>
            {
                e.Name = "InventoryQueue";
                e.PrefetchCount = 8;
            });
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<OrderState> sagaConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
            endpointConfigurator.UseInMemoryOutbox();

            var partition = endpointConfigurator.CreatePartitioner(8);

            sagaConfigurator.Message<InventoryQuantities>(x => x.UsePartitioner(partition, m => m.Message.OrderId));
            sagaConfigurator.Message<SubmitOrder>(x => x.UsePartitioner(partition, m => m.Message.OrderId));
            sagaConfigurator.Message<OrderAccepted>(x => x.UsePartitioner(partition, m => m.Message.OrderId));

            sagaConfigurator.UseMessageRetry(r => r.Immediate(5));
            sagaConfigurator.UseInMemoryOutbox();
        }
    }
}
