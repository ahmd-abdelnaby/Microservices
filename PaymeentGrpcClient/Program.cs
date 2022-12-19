// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

Console.ReadLine();
using var channel = GrpcChannel.ForAddress("https://localhost:5001");

var client = new InventoryGrpcService.InventoryServices.InventoryServicesClient(channel);

//var client = new PaymentGrpcService.PaymentServices.PaymentServicesClient(channel);

Console.WriteLine("Finished");
Console.ReadLine();
