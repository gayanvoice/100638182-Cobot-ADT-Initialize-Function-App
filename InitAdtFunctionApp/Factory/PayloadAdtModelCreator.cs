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
    internal class PayloadAdtModelCreator : IAdtModelCreator
    {
        public async Task<HttpResponseMessage> CreateAsync(string adtModelId)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            try
            {
                PayloadAdtModel payloadAdtModel = new PayloadAdtModel();
                payloadAdtModel.Id = adtModelId;
                payloadAdtModel.Mass = 0.0;
                payloadAdtModel.CogX = 0.0;
                payloadAdtModel.CogY = 0.0;
                payloadAdtModel.CogZ = 0.0;
                BasicDigitalTwin basicDigitalTwinModel = PayloadAdtModel.GetBasicDigitalTwin(payloadAdtModel: payloadAdtModel);
                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(payloadAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The '{payloadAdtModel.Id}' ADT model on '{payloadAdtModel.ModelId}' DTDL model created successfully.");
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
