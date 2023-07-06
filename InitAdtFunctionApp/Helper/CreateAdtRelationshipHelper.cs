using Azure.DigitalTwins.Core;
using Azure;
using InitAdtFunctionApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;

namespace InitAdtFunctionApp.Helper
{
    internal class CreateAdtRelationshipHelper
    {
        private HttpResponseHelper httpResponseHelper;
        private DigitalTwinsClient digitalTwinsClient;
        public CreateAdtRelationshipHelper(HttpResponseHelper httpResponseHelper)
        {
            this.httpResponseHelper = httpResponseHelper;
        }
        public void Create(string adtInstanceUrl)
        {
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
        }
        private async Task<HttpResponseMessage> CreateRelationshipAsync(DigitalTwinsClient client, string fromAdtModel, string toAdtModel)
        {
            try
            {
                var basicRelationship = new BasicRelationship
                {
                    TargetId = toAdtModel,
                };
                string relatioshipId = $"{fromAdtModel}-relationship->{toAdtModel}";
                await client.CreateOrReplaceRelationshipAsync<BasicRelationship>(fromAdtModel, relatioshipId, basicRelationship);
                return httpResponseHelper.CreateOkRequest(message: $"The relationship from 'Cobot' ADT model to 'ControlBox' has been created successfully.");
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
        public async Task<HttpResponseMessage> CobotToControlBoxAsync()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "Cobot", "ControlBox");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'Cobot' ADT model to 'ControlBox'.", exception: e);
            }
        }
    }
}
