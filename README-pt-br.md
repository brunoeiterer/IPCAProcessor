[![en](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/brunoeiterer/IPCAProcessor/blob/master/README.md)

# IPCAProcessor
IPCA (Índice Nacional de Preços ao Consumidor Amplo) é uma das medidas de inflação no Brasil. A série histórica do IPCA é disponibilizada pelo IBGE (Instituto Brasileiro de Geografia e Estatística), mas pode ser complicado processar os dados automaticamente por conta do formato disponiblizado. O IPCAProcessor é um aplicativo C# app para converter a série histórica em diversos formatos mais amigáveis, disponibilizando-os para pesquisa e/ou automações.

## Formatos Suportados
Atualmente, o IPCA Processor pode converter para os seguintes formatos, todos suportando filtrar a série histórica por períodos:
* JSON
* PNG Graph
* CSV
* SQL Dump

### Formatos Planejados
* Por enquanto nenhum outro formato está planejado. Caso deseje que algum formato seja suportado, por favor abra uma issue.

## Exemplos
O arquivo Program.cs disponível no projeto contém exemplos de como exportar os dados para todos os formatos disponíveis, e a pasta Output contém os dados formatados nos exemplos.

## Como rodar o projeto

### Pré-Requisitos
* .NET SDK 8 ou maior instalado

### Passo a Passo
* Clone o repositório:
```
git clone https://github.com/brunoeiterer/IPCAProcessor.git
cd IPCAProcessor
```
* Rode:
```
dotnet run
```
