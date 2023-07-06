using Azure.DigitalTwins.Core;

namespace AdtInitFunctionApp.Model.AdtModel
{
    internal class PayloadAdtModel
    {
        public string Id { get; set; } = "Payload";
        public string ModelId { get; set; } = "dtmi:com:Cobot:Payload;1";
        public double Mass { get; set; }
        public double CogX { get; set; }
        public double CogY { get; set; }
        public double CogZ { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(PayloadAdtModel payloadAdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = payloadAdtModel.Id,
                Metadata = { ModelId = payloadAdtModel.ModelId },
                Contents =
                    {
                        { "mass", payloadAdtModel.Mass },
                        { "cogx", payloadAdtModel.CogX },
                        { "cogy", payloadAdtModel.CogY },
                        { "cogz", payloadAdtModel.CogZ }
                    },
            };
        }
    }
}