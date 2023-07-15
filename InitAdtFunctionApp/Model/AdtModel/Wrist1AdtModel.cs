using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class Wrist1AdtModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; } = "dtmi:com:Cobot:JointLoad:Wrist1;1";
        public double Position { get; set; }
        public double Temperature { get; set; }
        public double Voltage { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(Wrist1AdtModel wrist1AdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = wrist1AdtModel.Id,
                Metadata = { ModelId = wrist1AdtModel.ModelId },
                Contents =
                    {
                        { "Position", wrist1AdtModel.Position },
                        { "Temperature", wrist1AdtModel.Temperature },
                        { "Voltage", wrist1AdtModel.Voltage }
                    },
            };
        }
    }
}