using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class CobotAdtModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; } = "dtmi:com:Cobot:Cobot;1";
        public double ElapsedTime { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(CobotAdtModel cobotAdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = cobotAdtModel.Id,
                Metadata = { ModelId = cobotAdtModel.ModelId },
                Contents =
                {
                    { "elapsed_time", cobotAdtModel.ElapsedTime }
                },
            };
        }
    }
}