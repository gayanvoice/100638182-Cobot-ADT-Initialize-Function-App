using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using CobotADTInitializeFunctionApp.Helper;
using CobotADTInitializeFunctionApp.Interface;
using System.Collections.Generic;
using CobotADTInitializeFunctionApp.Factory;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace CobotADTInitializeFunctionApp
{
    public static class CobotADTInitializeFunction
    {
        [FunctionName("UploadDTDLModelFunction")]
        public static async Task<HttpResponseMessage> UploadDTDLModelFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            string blobInstanceUrl = Environment.GetEnvironmentVariable("BlobInstanceUrl");
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            string name = req.Query["name"];
            if (blobInstanceUrl is null)
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'BlobInstanceUrl' parameter is required in the environment.");
            }
            if (adtInstanceUrl is null)
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
            }
            if (name is null)
            {
                return httpResponseHelper.CreateBadRequest(message: $"A valid 'name' parameter is required in the query string.");
            }
            string fileName = name + ".json";
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobInstanceUrl), defaultAzureCredential);
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            BlobContainerClient adtModelContainer = blobServiceClient.GetBlobContainerClient("adt-model-container");
            BlobClient blobClient = adtModelContainer.GetBlobClient(fileName);
            try
            {
                BlobDownloadResult blobDownloadResult = await blobClient.DownloadContentAsync();
                string blobContents = blobDownloadResult.Content.ToString();
                List<string> dtdlModels = new List<string> { blobContents };
                await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                return httpResponseHelper.CreateOkRequest(message: $"The '{name}' DTDL model uploaded successfully.");
            }
            catch (RequestFailedException e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Azure Digital Twin service error.", exception: e);
            }
        }
        [FunctionName("CreateADTModelFunction")]
        public static async Task<HttpResponseMessage> CreateADTModelFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            Dictionary<string, IAdtModelCreator> adtModelCreatorDictionary = new Dictionary<string, IAdtModelCreator>
            {
                { "Cobot", new CobotAdtModelCreator() },
                { "Base", new BaseAdtModelCreator() },
                { "ControlBox", new ControlBoxAdtModelCreator() },
                { "Elbow", new ElbowAdtModelCreator() },
                { "JointLoad", new JointLoadAdtModelCreator() },
                { "Payload", new PayloadAdtModelCreator() },
                { "Shoulder", new ShoulderAdtModelCreator() },
                { "Tool", new ToolAdtModelCreator() },
                { "Wrist1", new Wrist1AdtModelCreator() },
                { "Wrist2", new Wrist2AdtModelCreator() },
                { "Wrist3", new Wrist3AdtModelCreator() }
            };
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            string name = req.Query["name"];
            string id = req.Query["id"];
            if (adtInstanceUrl is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
            }
            if (name is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: "The 'name' parameter is required in the query string.");
            }
            if (id is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: "The 'id' parameter is required in the query string.");
            }
            if (adtModelCreatorDictionary.TryGetValue(name, out IAdtModelCreator creator))
            {
                return await creator.CreateAsync(id);
            }
            else
            {
                return new HttpResponseHelper().CreateBadRequest(message: "A valid 'name' parameter is required in the query string.");
            }
        }
        [FunctionName("CreateADTRelationshipFunction")]
        public static async Task<HttpResponseMessage> CreateADTRelationshipFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            AdtRelationshipCreator adtRelationshipCreator = new AdtRelationshipCreator();
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            string name = req.Query["name"];
            string from = req.Query["from"];
            string to = req.Query["to"];
            if (adtInstanceUrl is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
            }
            if (name is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: "The 'name' parameter is required in the query string.");
            }
            if (from is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: "The 'from' parameter is required in the query string.");
            }
            if (to is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: "The 'to' parameter is required in the query string.");
            }
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            try
            {
                var basicRelationship = new BasicRelationship
                {
                    TargetId = to,
                    Name = name
                };
                string relatioshipId = $"{from}-{name}-{to}";
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
        [FunctionName("DeleteADTRelationshipFunction")]
        public static async Task<HttpResponseMessage> DeleteADTRelationshipFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            AdtRelationshipCreator adtRelationshipCreator = new AdtRelationshipCreator();
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            string from = req.Query["from"];
            string id = req.Query["id"];
            if (adtInstanceUrl is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
            }
            if (from is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: "The 'from' parameter is required in the query string.");
            }
            if (id is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: "The 'id' parameter is required in the query string.");
            }
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            try
            {
                await digitalTwinsClient.DeleteRelationshipAsync(from, id);
                return httpResponseHelper.CreateOkRequest(message: $"The relationship '{id}' has been deleted successfully.");
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
        [FunctionName("DeleteADTModelFunction")]
        public static async Task<HttpResponseMessage> DeleteADTModelFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            AdtRelationshipCreator adtRelationshipCreator = new AdtRelationshipCreator();
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            string id = req.Query["id"];
            if (adtInstanceUrl is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
            }
            if (id is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: "The 'id' parameter is required in the query string.");
            }
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            try
            {
                await digitalTwinsClient.DeleteDigitalTwinAsync(id);
                return httpResponseHelper.CreateOkRequest(message: $"The '{id}' ADT model deleted successfully.");
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
        [FunctionName("GetADTModelFunction")]
        public static async Task<HttpResponseMessage> GetADTModelFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            AdtRelationshipCreator adtRelationshipCreator = new AdtRelationshipCreator();
            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            string id = req.Query["id"];
            if (adtInstanceUrl is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
            }
            if (id is null)
            {
                return new HttpResponseHelper().CreateBadRequest(message: "The 'id' parameter is required in the query string.");
            }
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
            try { 
                Response<BasicDigitalTwin> twinResponse = await digitalTwinsClient.GetDigitalTwinAsync<BasicDigitalTwin>(id);
                return httpResponseHelper.CreateOkRequest(message: System.Text.Json.JsonSerializer.Serialize(twinResponse));
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
