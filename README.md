# MetricsCore
### Description
The aim of the project is to create an app that must be able to be expanded with no code changes in its core(```MetricCore.csproj```). The idea of the project is to create 
text metrics checker that will be handy to use and can be added into any ASP.NET Core app with no extra coding(just add services and create metrics if needed).

### How to use
1. To register metrics use extension method for ```IServiceCollection``` called ```AddMetricsContainer```. 
Then you can chain calls ```AddMetric<SomeMetric>("SomeName")``` where ```SomeMetric``` is a class that implements ```IMetric``` interface and "SomeName" is an identifier that can be used to access a certain metric. 
2. To start using metrics inject ```IMetricsService``` into your controller/service and call ```RunMetricsCheck```. The first parameter is a string with text that needs to get through the metrics we have added in our ```Startup``` class, and the second (optional) string parameter represents a name of a certain metric. When only the first parameter was given it will return a collection of results, in the case when a certain name was entered it will return a single result.
