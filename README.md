# metricscore

1. To register metrics use extension method for ```IServiceCollection``` called ```AddMetricsContainer```. 
Then you can chain calls ```AddMetric<SomeMetric>("SomeName")``` where ```SomeMetric``` is a class that implements ```IMetric``` interface and "SomeName" is an 
identifier which can be used to access a certain metric.
2. To start using metrics add ```IMetricsService``` into your controller/service and call ```RunMetricsCheck```. The first parameter is a string with text that need to go 
through the metrics we have added in our ```Startup``` class,
and the second (optional) string parameter represents a name of a certain metric. 
In case when only a the first parameter was given it will return a collection of results, in case when certain name was entered it will return a single result.
