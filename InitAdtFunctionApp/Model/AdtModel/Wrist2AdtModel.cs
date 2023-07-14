using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class Wrist2AdtModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; } = "dtmi:com:Cobot:JointLoad:Wrist2;1";
        public double Position { get; set; }
        public double Temperature { get; set; }
        public double Voltage { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(Wrist2AdtModel wrist2AdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = wrist2AdtModel.Id,
                Metadata = { ModelId = wrist2AdtModel.ModelId },
                Contents =
                    {
                        { "position", wrist2AdtModel.Position },
                        { "temperature", wrist2AdtModel.Temperature },
                        { "voltage", wrist2AdtModel.Voltage }
                    },
            };
        }
    }
}