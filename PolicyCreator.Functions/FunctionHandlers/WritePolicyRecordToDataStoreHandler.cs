using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using PolicyCreator.Core;
using PolicyCreator.Functions.Infrastructure;

namespace PolicyCreator.Functions.FunctionHandlers
{
    public class WritePolicyRecordToDataStoreHandler
    {
        private const string TableEnvVariableName = "POLICY_TABLE_NAME";
        private const string AwsRegionEnvVariableName = "AWS_REGION";

        public async Task Handle(WritePolicyToDataStoreRequest request, ILambdaContext context)
        {
            var logger = context.Logger;
            var tableName = Environment.GetEnvironmentVariable(TableEnvVariableName);
            var regionCode = Environment.GetEnvironmentVariable(AwsRegionEnvVariableName);
            logger.LogLine($"Writing the Policy record to dynamo db table: {tableName}, Policy number: {request.PolicyNumber}");
            
            var client = new AmazonDynamoDBClient(new AmazonDynamoDBConfig {RegionEndpoint = RegionEndpoint.GetBySystemName(regionCode)});
            var document = new Document
            {
                [nameof(request.PolicyNumber)] = request.PolicyNumber,
                ["Quote"] = Document.FromJson(JsonConvert.SerializeObject(request.Quote))
            };
            await Table.LoadTable(client, tableName).PutItemAsync(document);
            
            logger.LogObject(request);
        }
    }
}
