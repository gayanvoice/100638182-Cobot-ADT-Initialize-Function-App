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
    internal class CreateAdtModelHelper
    {
        private HttpResponseHelper httpResponseHelper;
        private DigitalTwinsClient digitalTwinsClient;
        public CreateAdtModelHelper(HttpResponseHelper httpResponseHelper)
        {
            this.httpResponseHelper = httpResponseHelper;
        }
        public void Create(string adtInstanceUrl)
        {
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
        }
        public async Task<HttpResponseMessage> CobotAsync()
        {
            try
            {
                CobotDigitalTwinModel cobotDigitalTwinModel = new CobotDigitalTwinModel();
                cobotDigitalTwinModel.Id = "Cobot";
                cobotDigitalTwinModel.ElapsedTime = 0.0;

                BasicDigitalTwin cobotBasicDigitalTwinModel = CobotDigitalTwinModel
                    .GetBasicDigitalTwin(cobotDigitalTwinModel: cobotDigitalTwinModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(cobotDigitalTwinModel.Id, cobotBasicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:Cobot;1' ADT model uploaded successfully.");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: e.ToString());
            }
        }
    }
}
