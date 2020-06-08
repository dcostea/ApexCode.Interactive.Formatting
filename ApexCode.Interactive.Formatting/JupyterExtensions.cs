using Microsoft.ML.Data;

namespace ApexCode.Interactive.Formatting
{
    public static class JupyterExtensions
    {
        public static MulticlassClassificationMetricsDisplayView AddCategories(this MulticlassClassificationMetrics metrics, string[] categories)
        {
            return new MulticlassClassificationMetricsDisplayView { Metrics = metrics, Categories = categories };
        }

        public static ConfusionMatrixDisplayView AddCategories(this ConfusionMatrix confusionMatrix, string[] categories)
        {
            return new ConfusionMatrixDisplayView { ConfusionMatrix = confusionMatrix, Categories = categories };
        }
    }
}
