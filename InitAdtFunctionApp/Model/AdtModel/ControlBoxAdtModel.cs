using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class ControlBoxAdtModel
    {
        public string Id { get; set; } = "ControlBox";
        public string ModelId { get; set; } = "dtmi:com:Cobot:ControlBox;1";
        public double Voltage { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(ControlBoxAdtModel controlBoxAdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = controlBoxAdtModel.Id,
                Metadata = { ModelId = controlBoxAdtModel.ModelId },
                Contents =
                    {
                        { "voltage", controlBoxAdtModel.Voltage }
                    },
            };
        }
    }
}