using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Newtonsoft.Json;
using PolicyCreator.Core;
using PolicyCreator.Functions.InputDeclarations;
using PolicyCreator.Functions.OutputDeclarations;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]
namespace PolicyCreator.Functions.FunctionHandlers
{
    public class InvokeUnderwriteHandler
    {
        public async Task<UnderwriteInvocationResponse> Handle(InvokeUnderwriteRequest request, ILambdaContext context)
        {
            var logger = context.Logger;
            logger.LogLine($"Handling request for quote: {request.QuoteId} with Payload");
            logger.LogLine(JsonConvert.SerializeObject(request));
            logger.LogLine("Invoking underwrite service to get information");
            var response = new UnderwriteInvocationResponse
            {
                CorrelationId = request.CorrelationId,
                Quote = new Quote {Id = request.QuoteId}
            };
            
            logger.LogLine("Response received from Underwrite service");
            logger.LogLine(JsonConvert.SerializeObject(response));

            return await Task.FromResult(response);
        }
    }
}
