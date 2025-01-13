[![pt-br](https://img.shields.io/badge/lang-pt--br-green.svg)](https://github.com/brunoeiterer/IPCAProcessor/blob/master/README-pt-br.md)

# IPCAProcessor
IPCA (Índice Nacional de Preços ao Consumidor Amplo) is one of the measurements of inflation in Brazil. The historical data about IPCA is made available by IBGE (Instituto Brasileiro de Geografia e Estatística), but it can be cumbersome to process automatically due to the format available. IPCAProcessor is a C# app to convert IPCA historical data to several formats, making it available for research/automation.

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
