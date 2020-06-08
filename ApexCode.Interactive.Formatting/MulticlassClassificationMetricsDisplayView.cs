using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ApexCode.Interactive.Formatting
{
    public class MulticlassClassificationMetricsDisplayView
    {
        public MulticlassClassificationMetrics Metrics { get; set; }
        public string[] Categories { get; set; }
    }
}
