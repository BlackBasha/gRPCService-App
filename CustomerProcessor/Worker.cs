using CustomerService.gRPC;
using Grpc.Net.Client;
using static CustomerService.gRPC.CustomerService;

namespace CustomerProcessor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {

                var channel = GrpcChannel.ForAddress("http://localhost:5264");
                var client = new CustomerServiceClient(channel);
                var customerPacket = new CustomerPacket();
                for (int i = 0; i < 10; i++)
                {
                    var customerMsg = new CustomerMessage { Name = $"Ahmet{i}", Id = i, Address = $"Address{i}" };
                    customerPacket.CustomerMsg.Add(customerMsg);
                }

                var result = client.AddCustomer(customerPacket);
                if (result.Status == ReadStatus.Success)
                {
                    _logger.LogInformation("Done! client send data to service", DateTimeOffset.Now);
                }
                else
                {
                    _logger.LogError("Error! client could not send data to service", DateTimeOffset.Now);

                }


                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}