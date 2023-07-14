using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using CobotADTInitializeFunctionApp.Helper;

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
        [FunctionName("CreateADTRelationshipFunction")]
        public static async Task<HttpResponseMessage> CreateADTRelationshipFunction(
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
                        case "CobotToJointLoad":
                            return await createAdtRelationshipHelper.CobotToJointLoadAsync();
                        case "CobotToPayload":
                            return await createAdtRelationshipHelper.CobotToPayloadAsync();
                        case "JointLoadToBase":
                            return await createAdtRelationshipHelper.JointLoadToBaseAsync();
                        case "JointLoadToShoulder":
                            return await createAdtRelationshipHelper.JointLoadToShoulderAsync();
                        case "JointLoadToElbow":
                            return await createAdtRelationshipHelper.JointLoadToElbowAsync();
                        case "JointLoadToWrist1":
                            return await createAdtRelationshipHelper.JointLoadToWrist1Async();
                        case "JointLoadToWrist2":
                            return await createAdtRelationshipHelper.JointLoadToWrist2Async();
                        case "JointLoadToWrist3":
                            return await createAdtRelationshipHelper.JointLoadToWrist3Async();
                        case "JointLoadToTool":
                            return await createAdtRelationshipHelper.JointLoadToToolAsync();
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
