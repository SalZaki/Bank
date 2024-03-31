<a href="https://twitter.com/intent/follow?screen_name=sal_zaki"><img src="https://camo.githubusercontent.com/a4e7c9bc9e98548731968d0ea64f33ecb10231adef598d8d011e4056292052c4/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f2532302d547769747465722d2532333144413146323f6c6f676f3d74776974746572266c6f676f436f6c6f723d7768697465267374796c653d666f722d7468652d6261646765" data-canonical-src="https://img.shields.io/badge/%20-Twitter-%231DA1F2?logo=twitter&amp;logoColor=white&amp;style=for-the-badge" style="max-width: 100%;" alt="" /></a>
<a href="https://www.linkedin.com/in/sal-zaki-b39369172" rel="nofollow"><img src="https://camo.githubusercontent.com/fffc9c5f5340c6adbdc00b39d9dc9fcb2e5ea2f0974226acaa542e4524090c5e/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f2532302d4c696e6b6564496e2d2532333041363643323f6c6f676f3d6c696e6b6564696e266c6f676f436f6c6f723d7768697465267374796c653d666f722d7468652d6261646765266c696e6b3d68747470733a2f2f7777772e6c696e6b6564696e2e636f6d2f696e2f6d65686469686164656c69" data-canonical-src="https://img.shields.io/badge/%20-LinkedIn-%230A66C2?logo=linkedin&amp;logoColor=white&amp;style=for-the-badge&amp;link=https://www.linkedin.com/in/sal-zaki-b39369172" style="max-width: 100%;" alt="Sal Zaki's linkedin" /></a>
[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg?logoColor=white&style=for-the-badge)](http://commitizen.github.io/cz-cli/)

# Bank Microservice
This microservice provides banking functionalities through a clean architecture approach, utilizing Domain-Driven Design (DDD) principles, built using the latest version of .NET Core i.e. `.NET 8.0`

## Features
- Create new bank accounts
- Retrieve existing bank accounts
- Activate existing bank accounts
- Deactivate existing bank accounts

## Technologies Used
<a href="https://dotnet.microsoft.com/"><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="54" height="54" alt="dotnet" style="vertical-align:top; margin:4px;" /></a>
<a href="https://hub.docker.com/"><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/docker/docker-original-wordmark.svg" width="54" height="54" alt="docker" style="vertical-align:top; margin:4px" /></a>
<a href="https://datalust.co/"><img src="https://datalust.co/img/seq-logo-dark.svg" alt="seq" width="54" height="54" style="vertical-align:top; margin:4px;" /></a>

## Solution Structure

````
bank/
│
├── src/
│   ├── Payment.Bank.Api/
│   │   ├── Controllers/
│   │   ├── Extensions/
│   │   ├── Middlewares/
│   │   ├── Options/
│   │   ├── Swagger/
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── Startup.cs
│   │
│   ├── Payment.Bank.Application/
│   │   └── Accounts/
│   │       ├── Features/
│   │       │   ├── ActivateAccount/
│   │       │   ├── CreateAccount/
│   │       │   ├── DeactivateAccount/
│   │       │   └── GetAccount/
│   │       ├── Repositories/
│   │       └── Services/
│   │
│   ├── Payment.Bank.Common/
│   │   ├── Abstractions/
│   │   ├── Exceptions/
│   │   ├── Extensions/
│   │   ├── Mappers/
│   │   └── Utilities/
│   │
│   ├── Payment.Bank.Domain/
│   │   ├── Entities/
│   │   ├── Exceptions/
│   │   └── ValueObjects/
│   │
│   └── Payment.Bank.Infrastructure/
│       └── Repositories/
│
├── README.md
├── LICENSE
└── .gitignore
````

## Getting Started

### Prerequisites
- .NET Core SDK 8.0
- Docker installed on your machine.

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

### Build and run using Docker Compose

You can build and run whole application using [docker compose](https://docs.docker.com/compose/) from root folder:

````shell
docker-compose build
````

````shell
docker-compose up
````
![Docker](assets/Docker.png)

### AppSettings

#### Api Settings

``` config
"Api": {
  "Name": "Bank-Api",
  "Version": "1.0",
  "ApiVersionHeader": "x-api-version",
  "ReportApiVersions": true,
  "BaseUrl": "/api",
  "DocumentationUrl": "api/v1/documentation/",
  "BannerEnabled": true,
  "Authorization": {
    "ApiKey": "607F6A23-D130-46CD-A93C-6D9A6E5A8FB2"
  }
}
```

#### Swagger Settings

``` config
"Swagger": {
  "Enabled": true,
  "Name": "Bank-Api",
  "RoutePrefix": "api-docs",
  "RouteTemplate": "/api-docs/swagger/{documentName}/swagger.json",
  "EndpointPath": "/api-docs/swagger/{0}/swagger.json",
  "Info": {
    "Title": "Bank Api documentation",
    "Description": "Provides documentation for Bank Api.",
    "License": {
      "Name": "MIT License",
      "Url": "https://en.wikipedia.org/wiki/MIT_License"
    },
    "Contact": {
      "Name": "Sal Zaki",
      "Email": "salzaki@hotmail.com"
    }
  }
}
```

#### Feature Management Settings

``` config
"FeatureManagement": {
  "CreateAccount": true,
  "GetAccount": true,
  "DeactivateAccount": true,
  "ActivateAccount": true
}
```

## Account Controller with Feature Gate

``` csharp
[HttpGet("{accountNumber:int}", Name = "GetByAccountNumber")]
[FeatureGate(Constants.Features.GetAccount)]
[SwaggerOperation(
    OperationId = nameof(GetByAccountNumberAsync),
    Description = "Gets an account by account number.",
    Tags = ["Account"])]
[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetAccountResponse))]
[SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(BadRequest<ProblemDetails>))]
[SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
public async Task<Results<Ok<GetAccountResponse>, BadRequest<ProblemDetails>, NotFound>> GetByAccountNumberAsync(
  [FromServices] IAccountService accountService,
  [Required][FromRoute] int accountNumber,
  CancellationToken cancellationToken = default)
{
  ...
}
```
### Business Policies - Not implemented Yet

```csharp
public void Deposit(decimal amount)
{
  this.CheckPolicy(DepositCannotBeMadeToDeactivatedAccountPolicy);
  this.AccountBalance += amount;
}
```
## [Api Documentation](ApiDocumentation.md)

## Structured Logging

![Seq](assets/Seq-Dashboard.png)

![Seq](assets/Seq-StructuredLogging.png)

## Roadmap

List of features/tasks/approaches to add:

| Name                        | Status | Release date |
|-----------------------------|--------|--------------|
| Domain Unit Tests           | To do  | TBA          |
| Application Unit Tests      | To do  | TBA          |
| Integration Automated Tests | To do  | TBA          |

## Resources and References

### Seq
- [Seq Documentation](https://docs.datalust.co/docs/an-overview-of-seq)

### Domain-Driven Design

- ["Domain-Driven Design: Tackling Complexity in the Heart of Software"](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215) book, Eric Evans
- ["Implementing Domain-Driven Design"](https://www.amazon.com/Implementing-Domain-Driven-Design-Vaughn-Vernon/dp/0321834577) book, Vaughn Vernon
- ["Domain-Driven Design Distilled"](https://www.amazon.com/dp/0134434420) book, Vaughn Vernon
- ["Patterns, Principles, and Practices of Domain-Driven Design"](https://www.amazon.com/Patterns-Principles-Practices-Domain-Driven-Design-ebook/dp/B00XLYUA0W) book, Scott Millett, Nick Tune

#### Application Architecture

- ["Patterns of Enterprise Application Architecture"](https://martinfowler.com/books/eaa.html) book, Martin Fowler
- ["Dependency Injection Principles, Practices, and Patterns"](https://www.manning.com/books/dependency-injection-principles-practices-patterns) book, Steven van Deursen, Mark Seemann
- ["Clean Architecture: A Craftsman's Guide to Software Structure and Design (Robert C. Martin Series"](https://www.amazon.com/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164) book, Robert C. Martin
- ["The Clean Architecture"](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) article, Robert C. Martin

## License

- [MIT License](LICENSE)

## Fun Quote

> As you start to walk on the way, the way appears.
>
> **[Rumi](https://en.wikipedia.org/wiki/Rumi)**

