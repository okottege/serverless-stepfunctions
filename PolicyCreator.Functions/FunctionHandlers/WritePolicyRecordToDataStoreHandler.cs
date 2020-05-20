using System.Threading.Tasks;
using Amazon.Lambda.Core;
using PolicyCreator.Core;
using PolicyCreator.Functions.Infrastructure;

namespace PolicyCreator.Functions.FunctionHandlers
{
    public class WritePolicyRecordToDataStoreHandler
    {
        public async Task Handle(WritePolicyToDataStoreRequest request, ILambdaContext context)
        {
            var logger = context.Logger;
            logger.LogLine($"Writing the Policy record to dynamo db, Policy number: {request.PolicyNumber}");
            logger.LogObject(request);
        }
    }
}
