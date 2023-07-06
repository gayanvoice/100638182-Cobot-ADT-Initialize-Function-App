using Azure.DigitalTwins.Core;

namespace InitAdtFunctionApp.Model
{
    internal class ControlBoxDigitalTwinModel
    {
        public string Id { get; set; }
        public double Voltage { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(ControlBoxDigitalTwinModel controlBoxDigitalTwinModel)
        {
            return new BasicDigitalTwin
            {
                Id = controlBoxDigitalTwinModel.Id,
                Metadata = { ModelId = "dtmi:com:Cobot:ControlBox;1" },
                Contents =
                    {
                        { "voltage", controlBoxDigitalTwinModel.Voltage }
                    },
            };
        }
    }
}