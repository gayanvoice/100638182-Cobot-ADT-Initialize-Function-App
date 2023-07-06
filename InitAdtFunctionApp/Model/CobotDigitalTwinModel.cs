using Azure.DigitalTwins.Core;

namespace InitAdtFunctionApp.Model
{
    internal class CobotDigitalTwinModel
    {
        public string Id { get; set; }
        public double ElapsedTime { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(CobotDigitalTwinModel cobotDigitalTwinModel)
        {
            return new BasicDigitalTwin
            {
                Id = cobotDigitalTwinModel.Id,
                Metadata = { ModelId = "dtmi:com:Cobot:Cobot;1" },
                Contents =
                {
                    { "elapsed_time", cobotDigitalTwinModel.ElapsedTime }
                },
            };
        }
    }
}