<a href="https://twitter.com/intent/follow?screen_name=sal_zaki"><img src="https://camo.githubusercontent.com/646950fa95e17c65d499a33949a408692bb6b53d8b874e237fd501a915133af6/68747470733a2f2f696d672e736869656c64732e696f2f7374617469632f76313f7374796c653d666f722d7468652d6261646765266d6573736167653d5477697474657226636f6c6f723d314441314632266c6f676f3d54776974746572266c6f676f436f6c6f723d464646464646266c6162656c3d" data-canonical-src="https://img.shields.io/static/v1?style=for-the-badge&amp;message=Twitter&amp;color=1DA1F2&amp;logo=Twitter&amp;logoColor=FFFFFF&amp;label=" style="max-width: 100%;"/></a>
<a href="https://www.linkedin.com/in/sal-zaki-b39369172" rel="nofollow"><img src="https://camo.githubusercontent.com/e854c03004858cda907a5d097ecf9297b1bf3ad6b1ceb65e8d043c023c0077a1/68747470733a2f2f696d672e736869656c64732e696f2f7374617469632f76313f7374796c653d666f722d7468652d6261646765266d6573736167653d4c696e6b6564496e26636f6c6f723d304136364332266c6f676f3d4c696e6b6564496e266c6f676f436f6c6f723d464646464646266c6162656c3d" data-canonical-src="https://img.shields.io/static/v1?style=for-the-badge&amp;message=LinkedIn&amp;color=0A66C2&amp;logo=LinkedIn&amp;logoColor=FFFFFF&amp;label=" style="max-width: 100%;"/></a>
[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg?logoColor=white&style=for-the-badge)](http://commitizen.github.io/cz-cli/)

# Bank Microservice
This microservice provides banking functionalities through a clean architecture approach, utilizing Domain-Driven Design (DDD) principles, built using the latest version of .NET Core i.e. `.NET 8.0`

## Features
- Create a new bank account
- Retrieve an existing bank account
- Activate an existing bank account
- Deactivate an existing bank account

## Technologies Used
<a href="https://dotnet.microsoft.com/"><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="54" height="54" alt="dotnet" style="vertical-align:top; margin:4px;" /></a>
<a href="https://hub.docker.com/"><img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/docker/docker-original-wordmark.svg" width="54" height="54" alt="docker" style="vertical-align:top; margin:4px" /></a>
<a href="https://datalust.co/"><img src="https://datalust.co/img/seq-logo-dark.svg" alt="seq" width="54" height="54" style="vertical-align:top; margin:4px;" /></a>

## Solution Structure

````
📂 bank/
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

### Makefile Reference

```text
  build-docs                    🔨 Builds docs on local machine
  check-certs                   🔍 Checks development certs
  check                         🔍 Checks installed dependencies on local machine
  clean-certs                   🤖 Cleans up development certs
  clean-docs                    🧹 Cleans docs site
  clean                         🧹 Cleans up project
  docker-build                  🏃 Builds container using Docker compose
  docker-lint                   🐳 Lints Dockerfile
  docker-start                  🏃 Stars container using Docker compose
  docker-stop                   🏃 Stops container using Docker compose
  help                          💬 This help message
  install-certs                 🔐 Installs development certs
  install-docs                  🛠️ Installs necessary dependencies to build docs in Ruby
  lint-fix                      🔧 Lints & formats, fixes errors and modifies code
  lint                          🔎 Checks for linting and formatting errors in code
  run-docs                      🤖 Runs project docs (this listens for changes)
  serve-docs                    🏃️ Runs project docs (this does not listen for changes)
```

### Quick Start

Build the project using `make`, from the root of the project run:

````shell
make check-certs # run this to check is dev certs are installed
make install-certs # install dev certs
make docker-start
````

Open [https://localhost:5001](http://localhost:5000)

### Config Certificate
You can also configure certs manually by running the following commands to [Configure SSL](https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0) in your system:

#### Windows using Linux containers
```bash
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p password
dotnet dev-certs https --trust
```
***Note:** for running this command in `powershell` use `$env:USERPROFILE` instead of `%USERPROFILE%`*

#### macOS or Linux
```bash
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p $CREDENTIAL_PLACEHOLDER$
dotnet dev-certs https --trust
```

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

