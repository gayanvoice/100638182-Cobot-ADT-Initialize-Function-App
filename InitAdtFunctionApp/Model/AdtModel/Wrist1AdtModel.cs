using Azure.DigitalTwins.Core;

namespace AdtInitFunctionApp.Model.AdtModel
{
    internal class Wrist1AdtModel
    {
        public string Id { get; set; } = "Wrist1";
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
                        { "position", wrist1AdtModel.Position },
                        { "temperature", wrist1AdtModel.Temperature },
                        { "voltage", wrist1AdtModel.Voltage }
                    },
            };
        }
    }
}