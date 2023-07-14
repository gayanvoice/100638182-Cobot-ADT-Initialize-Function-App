using Azure.DigitalTwins.Core;
using Azure;
using CobotADTInitializeFunctionApp.Helper;
using CobotADTInitializeFunctionApp.Interface;
using CobotADTInitializeFunctionApp.Model.AdtModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Identity;
using System.Net;

namespace CobotADTInitializeFunctionApp.Factory
{
    internal class ControlBoxAdtModelCreator : IAdtModelCreator
    {
        public async Task<HttpResponseMessage> CreateAsync(string adtModelId)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            try
            {
                ControlBoxAdtModel controlBoxAdtModel = new ControlBoxAdtModel();
                controlBoxAdtModel.Id = adtModelId;
                controlBoxAdtModel.Voltage = 0.0;
                BasicDigitalTwin basicDigitalTwinModel = ControlBoxAdtModel.GetBasicDigitalTwin(controlBoxAdtModel: controlBoxAdtModel);
                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(controlBoxAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The '{controlBoxAdtModel.Id}' ADT model on '{controlBoxAdtModel.ModelId}' DTDL model created successfully.");
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
