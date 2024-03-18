---
layout: home
title: Overview
nav_order: 1
has_children: false
---

<a href="https://github.com/salzaki" alt="sal zaki's github"><img src="https://img.shields.io/static/v1?style=for-the-badge&message=GitHub&color=181717&logo=GitHub&logoColor=FFFFFF&label=" /></a>
<a href="https://www.linkedin.com/in/sal-zaki-b39369172/" alt="sal zaki's linkedin"><img src="https://img.shields.io/static/v1?style=for-the-badge&message=LinkedIn&color=0A66C2&logo=LinkedIn&logoColor=FFFFFF&label=" /></a>
<a href="https://twitter.com/sal_zaki" alt="sal zaki's twitter"><img src="https://img.shields.io/static/v1?style=for-the-badge&message=Twitter&color=1DA1F2&logo=Twitter&logoColor=FFFFFF&label=" /></a>

[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg?logoColor=white&style=for-the-badge)](http://commitizen.github.io/cz-cli/) [![build](https://github.com/salzaki/bank/actions/workflows/test-dotnet.yml/badge.svg)](https://github.com/salzaki/bank/actions/workflows/test-dotnet.yml)

# Bank Microservice
This microservice provides banking functionalities through a clean architecture approach, utilizing Domain-Driven Design (DDD) principles, built using the latest version of .NET Core i.e. `.NET 8.0`

## Features
- Create new bank accounts
- Retrieve information about existing accounts
- Update account details
- Transfer funds between accounts
- Close accounts

## Technologies Used
<a href="https://dotnet.microsoft.com/"><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="54" height="54" alt="dotnet" style="vertical-align:top; margin:4px;" /></a>
<a href="https://hub.docker.com/"><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/docker/docker-original-wordmark.svg" width="54" height="54" alt="docker" style="vertical-align:top; margin:4px" /></a>
<a href=""><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/typescript/typescript-original.svg"    alt="typescript" width="54" height="54" style="vertical-align:top; margin:4px;" /></a>

## Solution Structure

````
bank/
│
├── src/
│   ├── BankMicroservice.Api/
│   │   ├── Controllers/
│   │   ├── Models/
│   │   ├── Services/
│   │   └── Program.cs
│   │   └── Startup.cs
│   │
│   ├── BankMicroservice.Core/
│   │   ├── Entities/
│   │   ├── Repositories/
│   │   ├── Services/
│   │   └── Utilities/
│   │
│   └── BankMicroservice.Tests/
│       ├── Unit/
│       └── Integration/
│
├── README.md
├── LICENSE
└── .gitignore
````

## Getting Started

### Prerequisites
- .NET Core SDK [version]
- [Any other prerequisites]

### Installation
Clone the repository:

````shell
git clone https://github.com/salzaki/bank.git
````

Navigate to the project directory:
````shell
cd bank
````

Build the project:
````shell
dotnet build
````

Run the project:
````shell
dotnet run
````

## Docs

```csharp
app.MapPost("/Forecast/New", (ForecastRequestDto request, WeatherService weatherService) =>
{
    return weatherService.GetForecast(request).ToMinimalApiResult();
})
.WithName("NewWeatherForecast");
```

### License

[MIT License](LICENSE)

### Fun Qoute

> Do anything, but let it produce joy
>
> **[Walt Whitman](https://en.wikipedia.org/wiki/Walt_Whitman)**
