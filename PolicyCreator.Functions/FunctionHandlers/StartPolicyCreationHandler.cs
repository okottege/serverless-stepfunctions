using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.StepFunctions;
using Amazon.StepFunctions.Model;
using Newtonsoft.Json;
using PolicyCreator.Functions.Infrastructure;

namespace PolicyCreator.Functions.FunctionHandlers
{
    public class StartPolicyCreationHandler
    {
        private const string StepFunctionArnEnvVariableName = "STEP_FUNCTION_ARN";
        private const string QuoteIdPathParamName = "quoteId";

        public async Task<APIGatewayProxyResponse> Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var logger = context.Logger;
            logger.LogLine($"Handling request for {request.Path}");

            var quoteId = request.PathParameters[QuoteIdPathParamName];
            var correlationId = request.GetCorrelationId();
            logger.LogLine($"Quote Id: {quoteId}, CorrelationId: {correlationId}");

            var stepFunctionARN = Environment.GetEnvironmentVariable(StepFunctionArnEnvVariableName);
            var stepFunctionInput = JsonConvert.SerializeObject(new {QuoteId = quoteId, Correlationid = correlationId});
            var regionCode = Environment.GetEnvironmentVariable(GlobalEnvironmentVariables.RegionCode);
            
            var client = new AmazonStepFunctionsClient(new AmazonStepFunctionsConfig {RegionEndpoint = RegionEndpoint.GetBySystemName(regionCode)});
            await client.StartExecutionAsync(new StartExecutionRequest {StateMachineArn = stepFunctionARN, Input = stepFunctionInput});
            
            logger.LogLine("Request handled successfully.");
            return new APIGatewayProxyResponse {StatusCode = 200};
        }
    }
}
