
using CustomerService.gRPC;
using Grpc.Core;
using static CustomerService.gRPC.CustomerService;

namespace CustomerService.grpcServices
{
    public class GrpcCustomerSerivce: CustomerServiceBase
    {
        private ILogger<GrpcCustomerSerivce> _logger { get; }
        public GrpcCustomerSerivce(ILogger<GrpcCustomerSerivce> logger)
        {
            _logger = logger;
        }


        public override  Task<StatusResponse> AddCustomer(CustomerPacket request, ServerCallContext context)
        {
            try
            {
                foreach (var item in request.CustomerMsg)
                {
                    var customerMessage = new CustomerMessage { Id = item.Id, Name = item.Name, Address = item.Address };
                    //save to databse or processing it another way
                    _logger.LogInformation($"The message with the Id {customerMessage.Id} was successfully handeled");
                }

                return  Task.FromResult( new StatusResponse { Status = ReadStatus.Success, Message = "All the data handeled successfully." });  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"An exception happended when handeleing the data");
                return Task.FromResult( new StatusResponse { Status = ReadStatus.Failer, Message = "An exception happended when handeleing the data." });
            }
        }
    }
}
