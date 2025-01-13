# IPCAProcessor
A C# app to convert IPCA historical data to several formats

## Supported Formats
Currently, IPCA Processor is able to convert to the following formats:
* JSON, with the possibility of filtering by a specific period
* PNG Graph, it plots and generates a png file by the specified period

### Planned Formats
* CSV
* SQL Dump

## Examples
The Program.cs available in the project contains examples of how to export the data to all the available formats, and the Output folder contains the formatted data from the examples.

## How to run the project

### Prerequisites
* .NET SDK 8 or higher installed

### Steps
* Clone the repository:
```
git clone https://github.com/brunoeiterer/IPCAProcessor.git
cd IPCAProcessor
```
* Run:
```
dotnet run
```
