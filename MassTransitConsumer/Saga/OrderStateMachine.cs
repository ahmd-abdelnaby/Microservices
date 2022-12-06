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
            Event(() => SubmitOrder, x => x.CorrelateById(context => context.Message.OrderId));

            Initially(
                When(SubmitOrder)
                    .Then(x => x.Saga.OrderDate = x.Message.OrderDate)
                    .TransitionTo(Submitted),
                When(OrderAccepted)
                    .TransitionTo(Accepted));

            During(Submitted,
                When(OrderAccepted)
                    .TransitionTo(Accepted));

            During(Accepted,
                When(SubmitOrder)
                    .Then(x => x.Saga.OrderDate = x.Message.OrderDate));
        }

        public Event<SubmitOrder> SubmitOrder { get; private set; }
        public Event<OrderAccepted> OrderAccepted { get; private set; }
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
}
