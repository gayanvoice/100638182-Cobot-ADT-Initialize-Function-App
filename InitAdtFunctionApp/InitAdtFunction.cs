using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Azure;
using InitAdtFunctionApp.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Reflection.Metadata;
using Azure.Core;
using InitAdtFunctionApp.Helper;
using System.Diagnostics;

namespace InitAdtFunctionApp
{
    public static class InitAdtFunction
    {
        private static async Task<BasicDigitalTwin> CustomMethod_FetchAndPrintTwinAsync(string twinId, DigitalTwinsClient client)
        {
            // <GetTwin>
            BasicDigitalTwin twin;
            // <GetTwinCall>
            Response<BasicDigitalTwin> twinResponse = await client.GetDigitalTwinAsync<BasicDigitalTwin>(twinId);
            twin = twinResponse.Value;
            // </GetTwinCall>
            Console.WriteLine($"Model id: {twin.Metadata.ModelId}");
            foreach (string prop in twin.Contents.Keys)
            {
                if (twin.Contents.TryGetValue(prop, out object value))
                    Console.WriteLine($"Property '{prop}': {value}");
            }
            // </GetTwin>

            return twin;
        }
        [FunctionName("UploadDtdlModelFunction")]
        public static async Task<HttpResponseMessage> RunUploadDtdlModelFunction(
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

        [FunctionName("CreateAdtModelFunction")]
        public static async Task<IActionResult> RunCreateAdtModelFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            Console.WriteLine("Hello World!");

            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            string baseModelStorageUrl = Environment.GetEnvironmentVariable("BaseModelStorageUrl");
            string cobotModelStorageUrl = Environment.GetEnvironmentVariable("CobotModelStorageUrl");
            string controlBoxModelStorageUrl = Environment.GetEnvironmentVariable("ControlBoxModelStorageUrl");
            string elbowModelStorageUrl = Environment.GetEnvironmentVariable("ElbowModelStorageUrl");
            string payloadModelStorageUrl = Environment.GetEnvironmentVariable("PayloadModelStorageUrl");
            string shoulderModelStorageUrl = Environment.GetEnvironmentVariable("ShoulderModelStorageUrl");
            string toolModelStorageUrl = Environment.GetEnvironmentVariable("ToolModelStorageUrl");
            string wrist1ModelStorageUrl = Environment.GetEnvironmentVariable("Wrist1ModelStorageUrl");
            string wrist2ModelStorageUrl = Environment.GetEnvironmentVariable("Wrist2ModelStorageUrl");
            string wrist3ModelStorageUrl = Environment.GetEnvironmentVariable("Wrist3ModelStorageUrl");

            var credentials = new DefaultAzureCredential();
            var client = new DigitalTwinsClient(new Uri(adtInstanceUrl), credentials);
            Console.WriteLine($"Service client created – ready to go");

            Console.WriteLine($"Upload a model");
            string cobotDtdlModel = await new HttpClient().GetStringAsync(cobotModelStorageUrl);



            List<string> dtdlModels = new List<string>();
            dtdlModels.Add(cobotDtdlModel);


            await client.CreateModelsAsync(dtdlModels);


            string twinId = "Cobot";
            var initData = new BasicDigitalTwin
            {
                Id = twinId,
                Metadata = { ModelId = "dtmi:com:Cobot:Cobot;1" },
                Contents =
                {
                    { "ElapsedTime", 0.0 }
                },
            };

            await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(twinId, initData);
            // </CreateTwinCall>
            // </CreateTwin_withHelper>
            Console.WriteLine("Twin created successfully");

            //Print twin
            Console.WriteLine("--- Printing twin details:");
            await CustomMethod_FetchAndPrintTwinAsync(twinId, client);
            Console.WriteLine("--------");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
