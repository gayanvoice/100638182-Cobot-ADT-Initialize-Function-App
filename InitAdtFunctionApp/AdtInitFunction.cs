using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using InitAdtFunctionApp.Helper;

namespace InitAdtFunctionApp
{
    public static class AdtInitFunction
    {
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

        [FunctionName("CreateAdtModelFunction")]
        public static async Task<HttpResponseMessage> RunCreateAdtModelFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();

            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            if (adtInstanceUrl is not null)
            {

                CreateAdtModelHelper createAdtModelHelper = new CreateAdtModelHelper(httpResponseHelper: httpResponseHelper);
                createAdtModelHelper.Create(adtInstanceUrl: adtInstanceUrl);

                string adtModelName = req.Query["adtModelName"];
                if (adtModelName is not null)
                {
                    switch (adtModelName)
                    {
                        case "Cobot":
                            return await createAdtModelHelper.CobotAsync();
                        case "ControlBox":
                            return await createAdtModelHelper.ControlBoxAsync();
                        case "Payload":
                            return await createAdtModelHelper.PayloadAsync();
                        case "JointLoad":
                            return await createAdtModelHelper.JointLoadAsync();
                        case "Base":
                            return await createAdtModelHelper.BaseAsync();
                        case "Shoulder":
                            return await createAdtModelHelper.ShoulderAsync();
                        case "Elbow":
                            return await createAdtModelHelper.ElbowAsync();
                        case "Wrist1":
                            return await createAdtModelHelper.Wrist1Async();
                        case "Wrist2":
                            return await createAdtModelHelper.Wrist2Async();
                        case "Wrist3":
                            return await createAdtModelHelper.Wrist3Async();
                        case "Tool":
                            return await createAdtModelHelper.ToolAsync();
                        default:
                            return httpResponseHelper.CreateBadRequest(message: "A valid 'adtModelName' parameter is required in the query string.");
                    }
                }
                else
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'adtModelName' parameter is required in the query string.");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
            }
        }
        [FunctionName("CreateAdtRelationshipFunction")]
        public static async Task<HttpResponseMessage> RunCreateAdtRelationshipFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpResponseHelper httpResponseHelper = new HttpResponseHelper();

            string adtInstanceUrl = Environment.GetEnvironmentVariable("AdtInstanceUrl");
            if (adtInstanceUrl is not null)
            {

                CreateAdtRelationshipHelper createAdtRelationshipHelper = new CreateAdtRelationshipHelper(httpResponseHelper: httpResponseHelper);
                createAdtRelationshipHelper.Create(adtInstanceUrl: adtInstanceUrl);

                string adtRelationshipName = req.Query["adtRelationshipName"];
                if (adtRelationshipName is not null)
                {
                    switch (adtRelationshipName)
                    {
                        case "CobotToControlBox":
                            return await createAdtRelationshipHelper.CobotToControlBoxAsync();
                        case "ControlBox":
                            // return await createAdtRelationshipHelper.ControlBoxAsync();
                        default:
                            return httpResponseHelper.CreateBadRequest(message: "A valid 'adtRelationshipName' parameter is required in the query string.");
                    }
                }
                else
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'adtRelationshipName' parameter is required in the query string.");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'AdtInstanceUrl' parameter is required in the environment.");
            }
        }
    }
}
