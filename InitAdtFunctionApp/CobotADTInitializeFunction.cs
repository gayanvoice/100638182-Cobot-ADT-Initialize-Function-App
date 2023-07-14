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

            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            if (adtInstanceUrl is not null)
            {
                
                UploadDtdlModelHelper uploadDtdlModelHelper = new UploadDtdlModelHelper(httpResponseHelper: httpResponseHelper);
                uploadDtdlModelHelper.Create(adtInstanceUrl: adtInstanceUrl);

                string dtdlModelName = req.Query["dtdlModelName"];
                if (dtdlModelName is not null)
                {
                    switch (dtdlModelName)
                    {
                        case "Cobot":
                            return await uploadDtdlModelHelper.CobotAsync();
                        case "Base":
                            return await uploadDtdlModelHelper.BaseAsync();
                        case "ControlBox":
                            return await uploadDtdlModelHelper.ControlBoxAsync();
                        case "Elbow":
                            return await uploadDtdlModelHelper.ElbowAsync();
                        case "Payload":
                            return await uploadDtdlModelHelper.PayloadAsync();
                        case "Shoulder":
                            return await uploadDtdlModelHelper.ShoulderAsync();
                        case "Tool":
                            return await uploadDtdlModelHelper.ToolAsync();
                        case "Wrist1":
                            return await uploadDtdlModelHelper.Wrist1Async();
                        case "Wrist2":
                            return await uploadDtdlModelHelper.Wrist2Async();
                        case "Wrist3":
                            return await uploadDtdlModelHelper.Wrist3Async();
                        case "JointLoad":
                            return await uploadDtdlModelHelper.JointLoadAsync();
                        default:
                            return httpResponseHelper.CreateBadRequest(message: "A valid 'dtdlModelName' parameter is required in the query string.");
                    }
                }
                else
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'dtdlModelName' parameter is required in the query string.");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
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
            if (adtInstanceUrl is not null)
            {
                if (name is not null)
                {
                    if (id is not null)
                    {
                        if (adtModelCreatorDictionary.TryGetValue(name, out IAdtModelCreator creator))
                        {
                            return await creator.CreateAsync(id);
                        }
                        else
                        {
                            return new HttpResponseHelper().CreateBadRequest(message: "A valid 'name' parameter is required in the query string.");
                        }
                    }
                    else
                    {
                        return new HttpResponseHelper().CreateBadRequest(message: "The 'id' parameter is required in the query string.");
                    }
                }
                else
                {
                    return new HttpResponseHelper().CreateBadRequest(message: "The 'name' parameter is required in the query string.");
                }
            }
            else
            {
                return new HttpResponseHelper().CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
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
            if (adtInstanceUrl is not null)
            {
                if (name is not null)
                {
                    if (from is not null)
                    {
                        if (to is not null)
                        {
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
                        else
                        {
                            return new HttpResponseHelper().CreateBadRequest(message: "The 'to' parameter is required in the query string.");
                        }
                    }
                    else
                    {
                        return new HttpResponseHelper().CreateBadRequest(message: "The 'from' parameter is required in the query string.");
                    }
                }
                else
                {
                    return new HttpResponseHelper().CreateBadRequest(message: "The 'name' parameter is required in the query string.");
                }
            }
            else
            {
                return new HttpResponseHelper().CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
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
            string id = req.Query["id"];
            if (adtInstanceUrl is not null)
            {
                if (id is not null)
                {
                    DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
                    DigitalTwinsClient digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
                    try
                    {
                        AsyncPageable<BasicRelationship> baseRelationships = digitalTwinsClient.GetRelationshipsAsync<BasicRelationship>(id);
                        await foreach (BasicRelationship baseRelationship in baseRelationships)
                        {
                            await digitalTwinsClient.DeleteRelationshipAsync(id, baseRelationship.Id).ConfigureAwait(false);
                        }
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
                else
                {
                    return new HttpResponseHelper().CreateBadRequest(message: "The 'id' parameter is required in the query string.");
                }
            }
            else
            {
                return new HttpResponseHelper().CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
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
            if (adtInstanceUrl is not null)
            {
                if (id is not null)
                {
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
                else
                {
                    return new HttpResponseHelper().CreateBadRequest(message: "The 'id' parameter is required in the query string.");
                }
            }
            else
            {
                return new HttpResponseHelper().CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
            }
        }
    }
}
