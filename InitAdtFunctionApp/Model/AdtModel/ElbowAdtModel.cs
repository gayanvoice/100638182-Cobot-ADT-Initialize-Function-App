using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class ElbowAdtModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; } = "dtmi:com:Cobot:JointLoad:Elbow;1";
        public double Position { get; set; }
        public double Temperature { get; set; }
        public double Voltage { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(ElbowAdtModel elbowAdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = elbowAdtModel.Id,
                Metadata = { ModelId = elbowAdtModel.ModelId },
                Contents =
                    {
                        { "position", elbowAdtModel.Position },
                        { "temperature", elbowAdtModel.Temperature },
                        { "voltage", elbowAdtModel.Voltage },
                        { "x", elbowAdtModel.X },
                        { "y", elbowAdtModel.Y },
                        { "z", elbowAdtModel.Z }
                    },
            };
        }
    }
}