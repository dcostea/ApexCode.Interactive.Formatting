using Microsoft.ML.Data;

namespace ApexCode.Interactive.Formatting.UnitTests
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public float Temperature { get; set; }

        [LoadColumn(1)]
        public float Luminosity { get; set; }

        [LoadColumn(2)]
        public float Infrared { get; set; }

        [LoadColumn(3)]
        public float Distance { get; set; }

        [LoadColumn(4)]
        public string CreatedAt { get; set; }

        [ColumnName("Label"), LoadColumn(5)]
        public string Source { get; set; }
    }
}
