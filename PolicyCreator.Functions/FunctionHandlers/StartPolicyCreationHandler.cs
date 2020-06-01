using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using PolicyCreator.Functions.Infrastructure;

namespace PolicyCreator.Functions.FunctionHandlers
{
    public class StartPolicyCreationHandler
    {
        private const string QuoteIdPathParamName = "quoteId";
        private const string EventBusNameVariableName = "SALES_EVENTBUS_NAME";

        public async Task<APIGatewayProxyResponse> Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var logger = context.Logger;
            logger.LogLine($"Handling request for {request.Path}");

            var quoteId = request.PathParameters[QuoteIdPathParamName];
            var correlationId = request.GetCorrelationId();
            logger.LogLine($"[{correlationId}]: Quote Id: {quoteId}, CorrelationId: {correlationId}");

            var eventBusName = Environment.GetEnvironmentVariable(EventBusNameVariableName);
            var regionCode = Environment.GetEnvironmentVariable(GlobalEnvironmentVariables.RegionCode);

            var client = new AmazonEventBridgeClient(new AmazonEventBridgeConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(regionCode) });
            var eventEntry = new PutEventsRequestEntry
            {
                EventBusName = eventBusName,
                Detail = JsonConvert.SerializeObject(new {QuoteId = quoteId, Correlationid = correlationId}),
                Source = "Join",
                DetailType = "Start Policy Creation"
            };
            var putEventRequest = new PutEventsRequest {Entries = new[] {eventEntry}.ToList()};
            logger.LogLine($"[{correlationId}]: Raising event on event bus: {eventBusName} with Source: {eventEntry.Source}, DetailType: {eventEntry.DetailType}");
            var response = await client.PutEventsAsync(putEventRequest);

            LogErrors(logger, response, correlationId);
            LogSuccessfulRaisedEvents(logger, response, correlationId);

            logger.LogLine("Request handled successfully.");
            return new APIGatewayProxyResponse {StatusCode = 200};
        }

        private void LogErrors(ILambdaLogger logger, PutEventsResponse response, string correlationId)
        {
            var erroredEvents = response.Entries.Where(e => !string.IsNullOrWhiteSpace(e.ErrorMessage))
                .Select(e => new {e.ErrorCode, e.ErrorMessage})
                .ToList();

            if (!erroredEvents.Any()) return;

            logger.LogLine($"[{correlationId}]: Received following errors when raising events:");
            foreach (var error in erroredEvents)
            {
                logger.LogLine($"[{correlationId}]: Error Code: {error.ErrorCode} Error Message: {error.ErrorMessage}");
            }
        }

        private void LogSuccessfulRaisedEvents(ILambdaLogger logger, PutEventsResponse response, string correlationId)
        {
            var successfulEventIds = response.Entries
                .Where(e => string.IsNullOrWhiteSpace(e.ErrorMessage) && !string.IsNullOrWhiteSpace(e.EventId))
                .Select(e => e.EventId).ToList();
            if (!successfulEventIds.Any()) return;

            logger.LogLine($"[{correlationId}]: Successfully raised events: {string.Join(", ", successfulEventIds)}");
        }
    }
}
