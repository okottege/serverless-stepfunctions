using System;
using Amazon.Lambda.APIGatewayEvents;

namespace PolicyCreator.Functions.Infrastructure
{
    public static class ProxyRequestExtensionMethods
    {
        public static string GetCorrelationId(this APIGatewayProxyRequest request)
            => request.Headers.TryGetValue("x-correlation-id", out var correlationId) 
                ? correlationId 
                : Guid.NewGuid().ToString();
        
    }
}
