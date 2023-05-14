using AutoMapper;
using Discount.Grpc.Extensions;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();


builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build(); // Correct way for configuring host in net 6 IHost,WebApplicationBuilder
app.MigrateDatabase<Program>(); // as builder can build only once //var host = Host.CreateDefaultBuilder(args).Build();


// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
