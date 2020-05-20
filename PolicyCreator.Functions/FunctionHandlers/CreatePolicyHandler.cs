using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using PolicyCreator.Functions.Infrastructure;
using PolicyCreator.Functions.InputDeclarations;
using PolicyCreator.Functions.OutputDeclarations;

namespace PolicyCreator.Functions.FunctionHandlers
{
    public class CreatePolicyHandler
    {
        public async Task<CreatePolicyResponse> Handle(CreatePolicyRequest request, ILambdaContext context)
        {
            var logger = context.Logger;
            logger.LogLine($"Handling create policy for quote: {request.Quote.Id} with Payload:");
            logger.LogObject(request);
            logger.LogLine("Invoking create policy in BOA");
            var response = new CreatePolicyResponse
            {
                CorrelationId = request.CorrelationId, PolicyNumber = Guid.NewGuid().ToString(), Quote = request.Quote
            };
            logger.LogLine("Received response from BOA:");
            logger.LogObject(response);

            return await Task.FromResult(response);
        }
    }
}
