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
    internal class CobotAdtModelCreator : IAdtModelCreator
    {
        public async Task<HttpResponseMessage> CreateAsync(string adtModelId)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);

            try
            {
                CobotAdtModel cobotAdtModel = new CobotAdtModel();
                cobotAdtModel.Id = adtModelId;
                cobotAdtModel.ElapsedTime = 0.0;
                BasicDigitalTwin basicDigitalTwinModel = CobotAdtModel.GetBasicDigitalTwin(cobotAdtModel: cobotAdtModel);
                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(cobotAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The '{cobotAdtModel.Id}' ADT model on '{cobotAdtModel.ModelId}' DTDL model created successfully.");
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
