# ApexCode Interactive Object Formatters

##### Register needed objects like it follows:


# Jupyter Notebook
```
#r "nuget:ApexCode.Interactive.Formatting,0.0.1-alpha.5"
using ApexCode.Interactive.Formatting;
```
## DataFrame
```
Formatters.Register<DataFrame>();
display(df);
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
