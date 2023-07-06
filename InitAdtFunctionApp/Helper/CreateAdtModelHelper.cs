using Azure.DigitalTwins.Core;
using Azure;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Identity;
using AdtInitFunctionApp.Model.AdtModel;

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
                CobotAdtModel cobotAdtModel = new CobotAdtModel
                {
                    ElapsedTime = 0.0
                };
                BasicDigitalTwin basicDigitalTwinModel = CobotAdtModel
                    .GetBasicDigitalTwin(cobotAdtModel: cobotAdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(cobotAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:Cobot;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> ControlBoxAsync()
        {
            try
            {
                ControlBoxAdtModel controlBoxAdtModel = new ControlBoxAdtModel
                {
                    Voltage = 0.0
                };

                BasicDigitalTwin basicDigitalTwinModel = ControlBoxAdtModel.GetBasicDigitalTwin(controlBoxAdtModel: controlBoxAdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(controlBoxAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:ControlBox;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> PayloadAsync()
        {
            try
            {
                PayloadAdtModel payloadAdtModel = new PayloadAdtModel
                {
                    Mass = 0.0,
                    CogX = 0.0,
                    CogY = 0.0,
                    CogZ = 0.0
                };

                BasicDigitalTwin basicDigitalTwinModel = PayloadAdtModel.GetBasicDigitalTwin(payloadAdtModel: payloadAdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(payloadAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:Payload;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> JointLoadAsync()
        {
            try
            {
                JointLoadAdtModel jointLoadAdtModel = new JointLoadAdtModel();

                BasicDigitalTwin basicDigitalTwinModel = JointLoadAdtModel.GetBasicDigitalTwin(jointLoadAdtModel: jointLoadAdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(jointLoadAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:JointLoad;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> BaseAsync()
        {
            try
            {
                BaseAdtModel baseAdtModel = new BaseAdtModel
                {
                    Position = 0.0,
                    Temperature = 0.0,
                    Voltage = 0.0
                };

                BasicDigitalTwin basicDigitalTwinModel = BaseAdtModel.GetBasicDigitalTwin(baseAdtModel: baseAdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(baseAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:Base;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> ShoulderAsync()
        {
            try
            {
                ShoulderAdtModel shoulderAdtModel = new ShoulderAdtModel
                {
                    Position = 0.0,
                    Temperature = 0.0,
                    Voltage = 0.0
                };

                BasicDigitalTwin basicDigitalTwinModel = ShoulderAdtModel.GetBasicDigitalTwin(shoulderAdtModel: shoulderAdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(shoulderAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:JointLoad:Shoulder;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> ElbowAsync()
        {
            try
            {
                ElbowAdtModel elbowAdtModel = new ElbowAdtModel
                {
                    Position = 0.0,
                    Temperature = 0.0,
                    Voltage = 0.0,
                    X = 0.0,
                    Y = 0.0,
                    Z = 0.0,
                };

                BasicDigitalTwin basicDigitalTwinModel = ElbowAdtModel.GetBasicDigitalTwin(elbowAdtModel: elbowAdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(elbowAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:JointLoad:Elbow;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> Wrist1Async()
        {
            try
            {
                Wrist1AdtModel wrist1AdtModel = new Wrist1AdtModel
                {
                    Position = 0.0,
                    Temperature = 0.0,
                    Voltage = 0.0
                };

                BasicDigitalTwin basicDigitalTwinModel = Wrist1AdtModel.GetBasicDigitalTwin(wrist1AdtModel: wrist1AdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(wrist1AdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:JointLoad:Wrist1;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> Wrist2Async()
        {
            try
            {
                Wrist2AdtModel wrist2AdtModel = new Wrist2AdtModel
                {
                    Position = 0.0,
                    Temperature = 0.0,
                    Voltage = 0.0
                };

                BasicDigitalTwin basicDigitalTwinModel = Wrist2AdtModel.GetBasicDigitalTwin(wrist2AdtModel: wrist2AdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(wrist2AdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:JointLoad:Wrist2;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> Wrist3Async()
        {
            try
            {
                Wrist3AdtModel wrist3AdtModel = new Wrist3AdtModel
                {
                    Position = 0.0,
                    Temperature = 0.0,
                    Voltage = 0.0
                };

                BasicDigitalTwin basicDigitalTwinModel = Wrist3AdtModel.GetBasicDigitalTwin(wrist3AdtModel: wrist3AdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(wrist3AdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:JointLoad:Wrist3;1' ADT model created successfully.");
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
        public async Task<HttpResponseMessage> ToolAsync()
        {
            try
            {
                ToolAdtModel toolAdtModel = new ToolAdtModel
                {
                    Temperature = 0.0,
                    Voltage = 0.0,
                    X = 0.0,
                    Y = 0.0,
                    Z = 0.0,
                    Rx = 0.0,
                    Ry = 0.0,
                    Rz = 0.0
                };

                BasicDigitalTwin basicDigitalTwinModel = ToolAdtModel.GetBasicDigitalTwin(toolAdtModel: toolAdtModel);

                await digitalTwinsClient.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(toolAdtModel.Id, basicDigitalTwinModel);
                return httpResponseHelper.CreateOkRequest(message: $"The 'dtmi:com:Cobot:JointLoad:Tool;1' ADT model created successfully.");
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
