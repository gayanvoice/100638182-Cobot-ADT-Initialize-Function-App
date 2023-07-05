using Azure.DigitalTwins.Core;
using System.Text.Json.Serialization;

namespace InitAdtFunctionApp.Model
{
    internal class CobotDigitalTwinModel
    {
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinId)]
        public string Id { get; set; }

        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinETag)]
        public string ETag { get; set; }

        [JsonPropertyName("ElapsedTime")]
        public double ElapsedTime { get; set; }
    }
}
