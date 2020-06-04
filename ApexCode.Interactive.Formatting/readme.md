# ApexCode Interactive Object Formatters

##### Register needed objects like it follows:


``` 
Formatters.Register<DataFrame>();
```

``` 
// we need to provide an array with the categories
Formatters.Register<ConfusionMatrix>(categories);
```

``` 
// we need to provide an array with the categories
Formatters.Register<MulticlassClassificationMetrics>(categories);
```

``` 
Formatters.Register<List<TrainCatalogBase.CrossValidationResult<MulticlassClassificationMetrics>>>();
```
