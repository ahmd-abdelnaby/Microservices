using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitConsumer.Saga
{
    //public interface SubmitOrder :
    //CorrelatedBy<Guid>
    //{
    //    DateTime OrderDate { get; }
    //}

    //public class OrderSaga :
    //    ISaga,
    //    InitiatedBy<SubmitOrder>
    //{
    //    public Guid CorrelationId { get; set; }

    //    public DateTime? SubmitDate { get; set; }
    //    public DateTime? AcceptDate { get; set; }

    //    public async Task Consume(ConsumeContext<SubmitOrder> context)
    //    {
    //        SubmitDate = context.Message.OrderDate;
    //    }
    //}
}
