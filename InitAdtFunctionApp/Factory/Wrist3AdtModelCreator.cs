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
    internal class Wrist3AdtModelCreator : IAdtModelCreator
    {
        public async Task<HttpResponseMessage> CreateAsync(string adtModelId)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            try
            {
                Wrist3AdtModel wrist3AdtModel = new Wrist3AdtModel();
                wrist3AdtModel.Id = adtModelId;
                wrist3AdtModel.Position = 0.0;
                wrist3AdtModel.Temperature = 0.0;
                wrist3AdtModel.Voltage = 0.0;
                BasicDigitalTwin basicDigitalTwinModel = Wrist3AdtModel.GetBasicDigitalTwin(wrist3AdtModel: wrist3AdtModel);
                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(wrist3AdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The '{wrist3AdtModel.Id}' ADT model on '{wrist3AdtModel.ModelId}' DTDL model created successfully.");
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
