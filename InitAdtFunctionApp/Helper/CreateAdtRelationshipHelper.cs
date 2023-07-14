using Azure.DigitalTwins.Core;
using Azure;
using CobotADTInitializeFunctionApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;

namespace CobotADTInitializeFunctionApp.Helper
{
    internal class CreateAdtRelationshipHelper
    {
        private HttpResponseHelper httpResponseHelper;
        private DigitalTwinsClient digitalTwinsClient;
        public CreateAdtRelationshipHelper(HttpResponseHelper httpResponseHelper)
        {
            this.httpResponseHelper = httpResponseHelper;
        }
        public void Create(string adtInstanceUrl)
        {
            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true });
            digitalTwinsClient = new DigitalTwinsClient(new Uri(adtInstanceUrl), defaultAzureCredential);
        }
        private async Task<HttpResponseMessage> CreateRelationshipAsync(DigitalTwinsClient client, string fromAdtModel, string toAdtModel, string relationshipName)
        {
            try
            {
                var basicRelationship = new BasicRelationship
                {
                    TargetId = toAdtModel,
                    Name = relationshipName
                };
                string relatioshipId = $"{fromAdtModel}-{relationshipName}->{toAdtModel}";
                await client.CreateOrReplaceRelationshipAsync<BasicRelationship>(fromAdtModel, relatioshipId, basicRelationship);
                return httpResponseHelper.CreateOkRequest(message: $"The relationship from '{fromAdtModel}' ADT model to '{toAdtModel}' has been created successfully.");
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
        public async Task<HttpResponseMessage> CobotToControlBoxAsync()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "Cobot", "ControlBox", "contains_control_box");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'Cobot' ADT model to 'ControlBox'.", exception: e);
            }
        }
        public async Task<HttpResponseMessage> CobotToJointLoadAsync()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "Cobot", "JointLoad", "contains_joint_load");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'Cobot' ADT model to 'JointLoad'.", exception: e);
            }
        }
        public async Task<HttpResponseMessage> CobotToPayloadAsync()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "Cobot", "Payload", "contains_payload");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'Cobot' ADT model to 'Payload'.", exception: e);
            }
        }
        public async Task<HttpResponseMessage> JointLoadToBaseAsync()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "JointLoad", "Base", "contains_base");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'JointLoad' ADT model to 'Base'.", exception: e);
            }
        }
        public async Task<HttpResponseMessage> JointLoadToShoulderAsync()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "JointLoad", "Shoulder", "contains_shoulder");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'JointLoad' ADT model to 'Shoulder'.", exception: e);
            }
        }
        public async Task<HttpResponseMessage> JointLoadToElbowAsync()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "JointLoad", "Elbow", "contains_elbow");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'JointLoad' ADT model to 'Elbow'.", exception: e);
            }
        }
        public async Task<HttpResponseMessage> JointLoadToWrist1Async()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "JointLoad", "Wrist1", "contains_wrist1");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'JointLoad' ADT model to 'Wrist1'.", exception: e);
            }
        }
        public async Task<HttpResponseMessage> JointLoadToWrist2Async()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "JointLoad", "Wrist2", "contains_wrist2");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'JointLoad' ADT model to 'Wrist2'.", exception: e);
            }
        }
        public async Task<HttpResponseMessage> JointLoadToWrist3Async()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "JointLoad", "Wrist3", "contains_wrist3");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'JointLoad' ADT model to 'Wrist3'.", exception: e);
            }
        }
        public async Task<HttpResponseMessage> JointLoadToToolAsync()
        {
            try
            {
                return await CreateRelationshipAsync(digitalTwinsClient, "JointLoad", "Tool", "contains_tool");
            }
            catch (Exception e)
            {
                return httpResponseHelper.CreateBadRequest(message: $"Error creating relationship from 'JointLoad' ADT model to 'Tool'.", exception: e);
            }
        }
    }
}
