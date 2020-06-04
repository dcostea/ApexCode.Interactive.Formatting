using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApexCode.Interactive.Formatting.UnitTests
{
    public static class TestHelper
    {
        public static MLContext MLContext { get; set; } = new MLContext(0);

        public static (IDataView, IDataView) LoadData(string datasetPath)
        {
            IDataView data = MLContext.Data.LoadFromTextFile<ModelInput>(
                path: datasetPath,
                hasHeader: true,
                separatorChar: ',');

            var shuffledData = MLContext.Data.ShuffleRows(data, seed: 0);
            var split = MLContext.Data.TrainTestSplit(shuffledData, testFraction: 0.2);
            var trainingData = split.TrainSet;
            var testingData = split.TestSet;

            return (trainingData, testingData);
        }

        public static ITransformer TrainModel(IDataView trainingData)
        {
            MLContext mlContext = new MLContext(0);

            var featureColumns = new string[] { "Temperature", "Luminosity", "Infrared" };

            var trainingPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(mlContext.Transforms.Concatenate("Features", featureColumns))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = trainingPipeline.Fit(trainingData);

            return model;
        }
    }
}
