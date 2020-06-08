using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ApexCode.Interactive.Formatting
{
    public class ConfusionMatrixWithCategories
    {
        public ConfusionMatrix ConfusionMatrix { get; set; }
        public string[] Categories { get; set; }
    }

    public static class JupyterExtensions 
    {
        public static ConfusionMatrixWithCategories AddCategories(this ConfusionMatrix confusionMatrix, string[] categories)
        {
            return new ConfusionMatrixWithCategories { ConfusionMatrix = confusionMatrix, Categories = categories };
        }
    }
}
