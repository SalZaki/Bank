---
layout: home
title: Overview
nav_order: 1
has_children: false
---
<a href="https://twitter.com/intent/follow?screen_name=sal_zaki"><img src="https://camo.githubusercontent.com/a4e7c9bc9e98548731968d0ea64f33ecb10231adef598d8d011e4056292052c4/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f2532302d547769747465722d2532333144413146323f6c6f676f3d74776974746572266c6f676f436f6c6f723d7768697465267374796c653d666f722d7468652d6261646765" data-canonical-src="https://img.shields.io/badge/%20-Twitter-%231DA1F2?logo=twitter&amp;logoColor=white&amp;style=for-the-badge" style="max-width: 100%;" alt="" /></a>
<a href="https://www.linkedin.com/in/sal-zaki-b39369172" rel="nofollow"><img src="https://camo.githubusercontent.com/fffc9c5f5340c6adbdc00b39d9dc9fcb2e5ea2f0974226acaa542e4524090c5e/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f2532302d4c696e6b6564496e2d2532333041363643323f6c6f676f3d6c696e6b6564696e266c6f676f436f6c6f723d7768697465267374796c653d666f722d7468652d6261646765266c696e6b3d68747470733a2f2f7777772e6c696e6b6564696e2e636f6d2f696e2f6d65686469686164656c69" data-canonical-src="https://img.shields.io/badge/%20-LinkedIn-%230A66C2?logo=linkedin&amp;logoColor=white&amp;style=for-the-badge&amp;link=https://www.linkedin.com/in/sal-zaki-b39369172" style="max-width: 100%;" alt="Sal Zaki's linkedin" /></a>

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
