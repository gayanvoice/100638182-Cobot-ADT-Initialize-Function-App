﻿using Azure.DigitalTwins.Core;

namespace CobotADTInitializeFunctionApp.Model.AdtModel
{
    internal class BaseAdtModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; } = "dtmi:com:Cobot:JointLoad:Base;1";
        public double Position { get; set; }
        public double Temperature { get; set; }
        public double Voltage { get; set; }
        public static BasicDigitalTwin GetBasicDigitalTwin(BaseAdtModel baseAdtModel)
        {
            return new BasicDigitalTwin
            {
                Id = baseAdtModel.Id,
                Metadata = { ModelId = baseAdtModel.ModelId },
                Contents =
                    {
                        { "Position", baseAdtModel.Position },
                        { "Temperature", baseAdtModel.Temperature },
                        { "Voltage", baseAdtModel.Voltage }
                    },
            };
        }
    }
}