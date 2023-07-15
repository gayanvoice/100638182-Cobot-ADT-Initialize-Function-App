using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class ToolAdtModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; } = "dtmi:com:Cobot:JointLoad:Tool;1";
        public double Temperature { get; set; }
        public double Voltage { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Rx { get; set; }
        public double Ry { get; set; }
        public double Rz { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(ToolAdtModel toolAdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = toolAdtModel.Id,
                Metadata = { ModelId = toolAdtModel.ModelId },
                Contents =
                    {
                        { "Temperature", toolAdtModel.Temperature },
                        { "Voltage", toolAdtModel.Voltage },
                        { "X", toolAdtModel.X },
                        { "Y", toolAdtModel.Y },
                        { "Z", toolAdtModel.Z },
                        { "Rx", toolAdtModel.Rx },
                        { "Ry", toolAdtModel.Ry },
                        { "Rz", toolAdtModel.Rz }
                    },
            };
        }
    }
}