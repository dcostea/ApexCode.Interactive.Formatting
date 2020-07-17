# ApexCode Interactive Object Formatters

##### Register needed objects like it follows:


# Jupyter Notebook
```
#r "nuget:ApexCode.Interactive.Formatting"
using ApexCode.Interactive.Formatting;
```
## DataFrame, DataFrameColumn
```
Formatters.Register<DataFrame>();
display(df);

var columnRows = dataFrame.Columns[0];
display(columnRows);
```
## ConfusionMatrix (variant 1)
```
Formatters.Register<ConfusioMatrixDisplayView>();
var categories = new string[] { "FlashLight", "Infrared", "Day", "Lighter" };
display(metrics.ConfusioMatrix.AddCategories(categories));
```
## ConfusionMatrix (variant 2)
```
Formatters.Register<ConfusioMatrix>();
var categories = new string[] { "FlashLight", "Infrared", "Day", "Lighter" };
display(metrics.ConfusioMatrix(categories));.
```
