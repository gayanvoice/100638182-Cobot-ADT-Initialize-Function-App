using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class Wrist3AdtModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; } = "dtmi:com:Cobot:JointLoad:Wrist3;1";
        public double Position { get; set; }
        public double Temperature { get; set; }
        public double Voltage { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(Wrist3AdtModel wrist3AdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = wrist3AdtModel.Id,
                Metadata = { ModelId = wrist3AdtModel.ModelId },
                Contents =
                    {
                        { "position", wrist3AdtModel.Position },
                        { "temperature", wrist3AdtModel.Temperature },
                        { "voltage", wrist3AdtModel.Voltage }
                    },
            };
        }
    }
}