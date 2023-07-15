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
                        { "Position", wrist3AdtModel.Position },
                        { "Temperature", wrist3AdtModel.Temperature },
                        { "Voltage", wrist3AdtModel.Voltage }
                    },
            };
        }
    }
}