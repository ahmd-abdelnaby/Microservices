// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

Console.ReadLine();
using var channel = GrpcChannel.ForAddress("https://localhost:5001");

var client = new PaymentGrpcService.PaymentServices.PaymentServicesClient(channel);

var response = client.PaymentDetailsByOrderId(new PaymentGrpcService.OrderIdRequest() { Id = 2});

while (response.ResponseStream.MoveNext().Result)
{
    Console.WriteLine(response.ResponseStream.Current.Payment.ToString());
}

Console.WriteLine("Finished");
Console.ReadLine();
