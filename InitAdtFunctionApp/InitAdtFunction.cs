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
using System.Drawing;

namespace InitAdtFunctionApp
{
    public static class InitAdtFunction
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

                string dtdlModelName = req.Query["dtdlModelName"];
                if (dtdlModelName is not null)
                {
                    switch (dtdlModelName)
                    {
                        case "Cobot":
                            return await createAdtModelHelper.CobotAsync();
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
    }
}
