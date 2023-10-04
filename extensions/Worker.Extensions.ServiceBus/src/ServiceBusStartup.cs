using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Core;
using Microsoft.Azure.Functions.Worker.Extensions.Rpc;
using Microsoft.Azure.ServiceBus.Grpc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: WorkerExtensionStartup(typeof(ServiceBusStartup))]

namespace Microsoft.Azure.Functions.Worker;

public sealed class ServiceBusStartup : WorkerExtensionStartup
{
    public override void Configure(IFunctionsWorkerApplicationBuilder applicationBuilder)
    {
        applicationBuilder.Services.AddTransient(sp =>
        {
            IOptions<FunctionsGrpcOptions> options = sp.GetRequiredService<IOptions<FunctionsGrpcOptions>>();
            return new Settlement.SettlementClient(options.Value.CallInvoker);
        });
    }
}