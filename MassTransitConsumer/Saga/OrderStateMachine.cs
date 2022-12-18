using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitConsumer.Saga
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            //Event(() => SubmitOrder, x => x.CorrelateById(context => context.Message.OrderId));

            //Initially(
            //    When(SubmitOrder)
            //        .Then(x => x.Saga.OrderDate = x.Message.OrderDate)
            //        .TransitionTo(Submitted),
            //    When(OrderAccepted)
            //        .TransitionTo(Accepted));

            //During(Submitted,
            //    When(OrderAccepted)
            //        .TransitionTo(Accepted));

            Event(() => SubmitOrder, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderAccepted, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => UpdateInvetory, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => InventoryQuantitiesFailed, x => x.CorrelateById(context => context.Message.OrderId));
            
            Initially(
                  When(SubmitOrder)
                    .Then((context => Debugger.Break()))
                    .Then(x => x.Saga.OrderDate = x.Message.OrderDate)
                    .Publish(ctx => new InventoryQuantities { ProductQuantities = ctx.Message.ProductQuantities = ctx.Message.ProductQuantities, OrderId = ctx.Message.OrderId })
                    .TransitionTo(Submitted),
                 When(UpdateInvetory)
                    .Then((context => Debugger.Break()))
                    .Then((context => Debug.WriteLine("Update Inventory Event")))
                    .TransitionTo(InventoryUpdated),
                  
                When(OrderAccepted)
                    .TransitionTo(Accepted));;

            During(Submitted,
                When(InventoryQuantitiesFailed)
                    .Then((context => Debugger.Break()))
                    .Then((context => Debug.WriteLine("Update Inventory failed ,Order will be deleted using State OrderId")))
                    .TransitionTo(InventoryUpdated));

            During(Accepted,
                When(SubmitOrder)
                    .Then(x => x.Saga.OrderDate = x.Message.OrderDate));
        }

        public Event<SubmitOrder> SubmitOrder { get; private set; }
        public Event<OrderAccepted> OrderAccepted { get; private set; }
        public Event<InventoryQuantities> UpdateInvetory { get; private set; }
        public Event<InventoryQuantitiesFailed> InventoryQuantitiesFailed { get; private set; }
        public State Submitted { get; private set; }
        public State Accepted { get; private set; }
    }

    public interface SubmitOrder
    {
        Guid OrderId { get; }

        DateTime OrderDate { get; }
    }

    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }

        public DateTime? OrderDate { get; set; }
    }
    public class OrderStateMachineDefinition :
    SagaDefinition<OrderState>
    {
        public OrderStateMachineDefinition()
        {
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<OrderState> sagaConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(3, 1000));
            sagaConfigurator.UseInMemoryOutbox();
        }
    }
}
