using NUnit.Framework;
using Microsoft.DotNet.Interactive.Formatting;
using FluentAssertions;
using Microsoft.AspNetCore.Html;
using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML.Data;
using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;
using Microsoft.ML;

namespace ApexCode.Interactive.Formatting.UnitTests
{
    public class FormattersUT
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DataFrame_InputDataFrame_ReturnsHtml()
        {
            //Arrange
            PrimitiveDataFrameColumn<int> ints = new PrimitiveDataFrameColumn<int>("Ints", 3); // Makes a column of length 3. Filled with nulls initially
            StringDataFrameColumn strings = new StringDataFrameColumn("Strings", 3); // Makes a column of length 3. Filled with nulls initially
            DataFrame df = new DataFrame(ints, strings); // This will throw if the columns are of different lengths

            //Act
            Formatters.Register<DataFrame>();

            //Assert
            df.ToDisplayString("text/html").Should().Be("<table><thead><th><i>index</i></th><th>Ints</th><th>Strings</th></thead><tbody><tr><td>0</td><td>&lt;null&gt;</td><td>&lt;null&gt;</td></tr><tr><td>1</td><td>&lt;null&gt;</td><td>&lt;null&gt;</td></tr><tr><td>2</td><td>&lt;null&gt;</td><td>&lt;null&gt;</td></tr></tbody></table>");
        }

        const string DATASET_PATH = "./sensors_data.csv";

        [Test]
        public void MulticlassClassificationMetrics_ValidCategories_ReturnsHtml()
        {
            //Arrange
            var (trainingData, testingData) = TestHelper.LoadData(DATASET_PATH);
            var model = TestHelper.TrainModel(trainingData);
            var predictions = model.Transform(testingData);
            var metrics = TestHelper.MLContext.MulticlassClassification.Evaluate(predictions, "Label", "Score", "PredictedLabel");

            var categories = new string[] { "FlashLight", "Infrared", "Day", "Lighter" };
            
            //Act
            Formatters.Register<MulticlassClassificationMetrics>(categories);

            //Assert
            metrics.ToDisplayString("text/html").Should().Contain("<table><thead><th><b>EVALUATION: multi-class classification</b></th><th><b>Class</b></th><th><b>Value</b></th><th><b>Note</b></th></thead>");
        }

        [Test]
        public void MulticlassClassificationMetrics_MissingCategories_ReturnsHtml()
        {
            //Arrange
            var (trainingData, testingData) = TestHelper.LoadData(DATASET_PATH);
            var model = TestHelper.TrainModel(trainingData);
            var predictions = model.Transform(testingData);
            var metrics = TestHelper.MLContext.MulticlassClassification.Evaluate(predictions, "Label", "Score", "PredictedLabel");

            //Act
            Formatters.Register<MulticlassClassificationMetrics>();

            //Assert
            metrics.ToDisplayString("text/html").Should().Contain("The number of classes by Correlation Matrix (4) does not match the number of categories argument ()");
        }

        [Test]
        public void ConfusionMatrix_ValidCategories_ReturnsHtml()
        {
            //Arrange
            var (trainingData, testingData) = TestHelper.LoadData(DATASET_PATH);
            var model = TestHelper.TrainModel(trainingData);
            var predictions = model.Transform(testingData);
            var metrics = TestHelper.MLContext.MulticlassClassification.Evaluate(predictions, "Label", "Score", "PredictedLabel");

            var categories = new string[] { "FlashLight", "Infrared", "Day", "Lighter" };

            //Act
            Formatters.Register<ConfusionMatrix>(categories);

            //Assert
            metrics.ConfusionMatrix.ToDisplayString("text/html").Should().Contain(@"<table style=""margin: 50px; ""><tbody><tr style=""background-color: transparent""><td colspan=""2"" rowspan=""2"" style=""padding: 8px; background-color: lightsteelblue; text-align: center; "">Confusion Matrix</td>");
        }

        [Test]
        public void ConfusionMatrixWithCategories_ValidCategories_ReturnsHtml()
        {
            //Arrange
            var (trainingData, testingData) = TestHelper.LoadData(DATASET_PATH);
            var model = TestHelper.TrainModel(trainingData);
            var predictions = model.Transform(testingData);
            var metrics = TestHelper.MLContext.MulticlassClassification.Evaluate(predictions, "Label", "Score", "PredictedLabel");
            var categories = new string[] { "FlashLight", "Infrared", "Day", "Lighter" };
            var displayString = @"<table style=""margin: 50px; ""><tbody><tr style=""background-color: transparent""><td colspan=""2"" rowspan=""2"" style=""padding: 8px; background-color: lightsteelblue; text-align: center; "">Confusion Matrix</td>";
            Formatters.Register<ConfusionMatrixWithCategories>();

            //Act
            var confusionMatrixWithCategories = metrics.ConfusionMatrix.AddCategories(categories);
            var expectedString = confusionMatrixWithCategories.ToDisplayString("text/html");

            //Assert
            expectedString.Should().Contain(displayString);
        }

        [Test]
        public void ConfusionMatrix_MissingCategories_ReturnsHtml()
        {
            //Arrange
            var (trainingData, testingData) = TestHelper.LoadData(DATASET_PATH);
            var model = TestHelper.TrainModel(trainingData);
            var predictions = model.Transform(testingData);
            var metrics = TestHelper.MLContext.MulticlassClassification.Evaluate(predictions, "Label", "Score", "PredictedLabel");

            //Act
            Formatters.Register<ConfusionMatrix>();

            //Assert
            metrics.ConfusionMatrix.ToDisplayString("text/html").Should().Be("The number of classes in the Confusion Matrix (4) does not match the number of categories argument ()");
        }

        [Test]
        public void ConfusionMatrixWithCategories_MissingCategories_ReturnsHtml()
        {
            //Arrange
            var (trainingData, testingData) = TestHelper.LoadData(DATASET_PATH);
            var model = TestHelper.TrainModel(trainingData);
            var predictions = model.Transform(testingData);
            var metrics = TestHelper.MLContext.MulticlassClassification.Evaluate(predictions, "Label", "Score", "PredictedLabel");

            //Act
            Formatters.Register<ConfusionMatrixWithCategories>();

            //Assert
            metrics.ConfusionMatrix.AddCategories(null).ToDisplayString("text/html").Should().Be("The number of classes in the Confusion Matrix (4) does not match the number of categories argument ()");
        }
    }
}