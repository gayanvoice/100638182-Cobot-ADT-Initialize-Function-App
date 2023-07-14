using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class JointLoadAdtModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; } = "dtmi:com:Cobot:JointLoad;1";
        public static BasicDigitalTwin GetBasicDigitalTwin(JointLoadAdtModel jointLoadAdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = jointLoadAdtModel.Id,
                Metadata = { ModelId = jointLoadAdtModel.ModelId }
            };
        }
    }
}