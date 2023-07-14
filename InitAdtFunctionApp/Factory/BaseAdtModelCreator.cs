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
    internal class BaseAdtModelCreator : IAdtModelCreator
    {
        public async Task<HttpResponseMessage> CreateAsync(string id)
        {
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            try
            {
                BaseAdtModel baseAdtModel = new BaseAdtModel();
                baseAdtModel.Id = id;
                baseAdtModel.Position = 0.0;
                baseAdtModel.Temperature = 0.0;
                baseAdtModel.Voltage = 0.0;
                BasicDigitalTwin basicDigitalTwinModel = BaseAdtModel.GetBasicDigitalTwin(baseAdtModel: baseAdtModel);
                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(baseAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The '{baseAdtModel.Id}' ADT model on '{baseAdtModel.ModelId}' DTDL model created successfully.");
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
