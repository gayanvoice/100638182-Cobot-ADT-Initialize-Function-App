using Azure.DigitalTwins.Core;
using Azure;
using CobotADTInitializeFunctionApp.Helper;
using CobotADTInitializeFunctionApp.Interface;
using CobotADTInitializeFunctionApp.Model.AdtModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Identity;

namespace CobotADTInitializeFunctionApp.Factory
{
    public class AdtRelationshipCreator
    {
        public async Task<HttpResponseMessage> CreateAsync(string name, string from, string to)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            try
            {
                var basicRelationship = new BasicRelationship
                {
                    TargetId = to,
                    Name = name
                };
                string relatioshipId = $"{from}-{name}->{to}";
                await digitalTwinsClient.CreateOrReplaceRelationshipAsync<BasicRelationship>(from, relatioshipId, basicRelationship);
                return httpResponseHelper.CreateOkRequest(message: $"The relationship from '{from}' ADT model to '{to}' has been created successfully.");
            }
            catch (RequestFailedException e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Azure Digital Twin service error.", exception: e);
            }
            catch (ArgumentNullException e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"The 'digitalTwinId' or 'relationshipId' is null.", exception: e);
            }
        }
    }
}
