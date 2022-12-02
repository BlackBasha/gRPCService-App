using CustomerService.gRPC;
using CustomerService.grpcServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(x=>x.EnableDetailedErrors=true);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<GrpcCustomerSerivce>();

app.Run();
