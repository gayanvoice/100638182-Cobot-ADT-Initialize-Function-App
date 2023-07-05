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
    internal class UploadDtdlModelHelper
    {
        private HttpResponseHelper httpResponseHelper;
        private DigitalTwinsClient digitalTwinsClient;
        public UploadDtdlModelHelper(HttpResponseHelper httpResponseHelper)
        {
            this.httpResponseHelper = httpResponseHelper;
        }
        public void Create(string adtInstanceUrl)
        {
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential();
            digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
        }
        public async Task<HttpResponseMessage> CobotAsync()
        {
            string cobotDtdlModelStorageUrl = Environment.GetEnvironmentVariable("CobotDtdlModelStorageUrl");
            if (cobotDtdlModelStorageUrl is not null)
            {
                try
                {
                    string cobotDtdlModel = await new HttpClient().GetStringAsync(cobotDtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string>{ cobotDtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'Cobot' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'Cobot' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'CobotDtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
        public async Task<HttpResponseMessage> BaseAsync()
        {
            string baseDtdlModelStorageUrl = Environment.GetEnvironmentVariable("BaseDtdlModelStorageUrl");
            if (baseDtdlModelStorageUrl is not null)
            {
                try
                {
                    string baseDtdlModel = await new HttpClient().GetStringAsync(baseDtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string> { baseDtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'Base' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'Base' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'BaseDtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
        public async Task<HttpResponseMessage> ControlBoxAsync()
        {
            string controlBoxDtdlModelStorageUrl = Environment.GetEnvironmentVariable("ControlBoxDtdlModelStorageUrl");
            if (controlBoxDtdlModelStorageUrl is not null)
            {
                try
                {
                    string controlBoxDtdlModel = await new HttpClient().GetStringAsync(controlBoxDtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string> { controlBoxDtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'ControlBox' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'ControlBox' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'ControlBoxDtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
        public async Task<HttpResponseMessage> ElbowAsync()
        {
            string elbowDtdlModelStorageUrl = Environment.GetEnvironmentVariable("ElbowDtdlModelStorageUrl");
            if (elbowDtdlModelStorageUrl is not null)
            {
                try
                {
                    string elbowDtdlModel = await new HttpClient().GetStringAsync(elbowDtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string> { elbowDtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'Elbow' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'Elbow' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'ElbowDtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
        public async Task<HttpResponseMessage> PayloadAsync()
        {
            string payloadDtdlModelStorageUrl = Environment.GetEnvironmentVariable("PayloadDtdlModelStorageUrl");
            if (payloadDtdlModelStorageUrl is not null)
            {
                try
                {
                    string payloadDtdlModel = await new HttpClient().GetStringAsync(payloadDtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string> { payloadDtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'Payload' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'Payload' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'PayloadDtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
        public async Task<HttpResponseMessage> ShoulderAsync()
        {
            string shoulderDtdlModelStorageUrl = Environment.GetEnvironmentVariable("ShoulderDtdlModelStorageUrl");
            if (shoulderDtdlModelStorageUrl is not null)
            {
                try
                {
                    string shoulderDtdlModel = await new HttpClient().GetStringAsync(shoulderDtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string> { shoulderDtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'Shoulder' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'Shoulder' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'ShoulderDtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
        public async Task<HttpResponseMessage> ToolAsync()
        {
            string toolDtdlModelStorageUrl = Environment.GetEnvironmentVariable("ToolDtdlModelStorageUrl");
            if (toolDtdlModelStorageUrl is not null)
            {
                try
                {
                    string toolDtdlModel = await new HttpClient().GetStringAsync(toolDtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string> { toolDtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'Tool' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'Tool' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'ToolDtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
        public async Task<HttpResponseMessage> Wrist1Async()
        {
            string wrist1DtdlModelStorageUrl = Environment.GetEnvironmentVariable("Wrist1DtdlModelStorageUrl");
            if (wrist1DtdlModelStorageUrl is not null)
            {
                try
                {
                    string wrist1DtdlModel = await new HttpClient().GetStringAsync(wrist1DtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string> { wrist1DtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'Wrist1' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'Wrist1' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'Wrist1DtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
        public async Task<HttpResponseMessage> Wrist2Async()
        {
            string wrist2DtdlModelStorageUrl = Environment.GetEnvironmentVariable("Wrist2DtdlModelStorageUrl");
            if (wrist2DtdlModelStorageUrl is not null)
            {
                try
                {
                    string wrist2DtdlModel = await new HttpClient().GetStringAsync(wrist2DtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string> { wrist2DtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'Wrist2' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'Wrist2' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'Wrist2DtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
        public async Task<HttpResponseMessage> Wrist3Async()
        {
            string wrist3DtdlModelStorageUrl = Environment.GetEnvironmentVariable("Wrist3DtdlModelStorageUrl");
            if (wrist3DtdlModelStorageUrl is not null)
            {
                try
                {
                    string wrist3DtdlModel = await new HttpClient().GetStringAsync(wrist3DtdlModelStorageUrl);
                    List<string> dtdlModels = new List<string> { wrist3DtdlModel };
                    await digitalTwinsClient.CreateModelsAsync(dtdlModels);
                    return httpResponseHelper.CreateOkRequest(message: $"The 'Wrist3' DTDL model uploaded successfully.");
                }
                catch (RequestFailedException)
                {
                    return httpResponseHelper.CreateBadRequest(message: "The 'Wrist3' DTDL model already exists");
                }
            }
            else
            {
                return httpResponseHelper.CreateBadRequest(message: $" A valid 'Wrist3DtdlModelStorageUrl' parameter is required in the environment.");
            }
        }
    }
}
