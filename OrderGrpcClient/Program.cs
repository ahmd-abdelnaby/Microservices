// See https://aka.ms/new-console-template for more information
using Google.Protobuf.Collections;
using Grpc.Net.Client;
using InventoryGrpcService;

Console.WriteLine("Hello, World!");


using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new InventoryGrpcService.InventoryServices.InventoryServicesClient(channel);

ProductModelRequest productModelRequest = new ProductModelRequest();

productModelRequest.ProductModel.Add(new InventoryGrpcService.ProductModel { ProductId = 1, Quantity = 2 });
productModelRequest.ProductModel.Add(new InventoryGrpcService.ProductModel { ProductId = 2, Quantity = 7 });


var response = client.CheckAvalibleProductQuntity(productModelRequest);
Console.ReadLine();


