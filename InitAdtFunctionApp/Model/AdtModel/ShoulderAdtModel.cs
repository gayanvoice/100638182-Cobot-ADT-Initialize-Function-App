using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class ShoulderAdtModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; } = "dtmi:com:Cobot:JointLoad:Shoulder;1";
        public double Position { get; set; }
        public double Temperature { get; set; }
        public double Voltage { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(ShoulderAdtModel shoulderAdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = shoulderAdtModel.Id,
                Metadata = { ModelId = shoulderAdtModel.ModelId },
                Contents =
                    {
                        { "Position", shoulderAdtModel.Position },
                        { "Temperature", shoulderAdtModel.Temperature },
                        { "Voltage", shoulderAdtModel.Voltage }
                    },
            };
        }
    }
}