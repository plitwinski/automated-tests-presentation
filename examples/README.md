# How to run examples

## prerequisites

### example 01, 02, 04, 05
* .NET 6 SDK (or higher)

### example 03
* nodejs  6.9.5 (or higher)
* firefox 57.0.4 (or higher)

## example 01 - coarse grained unit tests
#### run following commands
* dotnet restore
* cd Example.CoarseGrainedUTest.Tests 
* dotnet test
#### or
#### open, compile and run via test explorer in Visual Studio (Windows only)

## example 02 - in-memory dependencies
#### run following commands
* dotnet restore
* cd Example.InMemoryDependencies.Tests
* dotnet test
#### or
#### open, compile and run via test explorer in Visual Studio (Windows only)

## example 03 - local hosting + headless firefox + json-server
#### run following commands
* npm run startWebServer (in separete process / console instance)
* npm run startApi (in separete process / console instance)
* npm run selenium-test (in separete process / console instance)

## example 04 - scenario testing
#### run following commands
* dotnet restore
* cd Example.ScenarioTesting.Tests
* dotnet test
#### or
#### open, compile and run via test explorer in Visual Studio (Windows only)

## example 05 - modularity
#### run following commands
* dotnet restore
* cd Example.Modularity.Tests
* dotnet test
#### or
#### open, compile and run via test explorer in Visual Studio (Windows only)