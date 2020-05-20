using Amazon.Lambda.Core;
using Newtonsoft.Json;

namespace PolicyCreator.Functions.Infrastructure
{
    public static class LoggingExtensions
    {
        public static void LogObject(this ILambdaLogger logger, object obj)
        {
            logger.LogLine(JsonConvert.SerializeObject(obj));
        }
    }
}
