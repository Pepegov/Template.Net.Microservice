# Microservice Template

This project allows you to quickly start developing a microservice application

The template idea was taken from [Calabonga](https://github.com/Calabonga/Microservice-Template)

<br>

---

<br>

## How to install template

To install a template for your device via dotnet:

1. Go to the directory with the desired template
    ```bash
    cd Template.Net.Microservice/src
    ```
2. Perform the installation
    ```bash
    dotnet new --install .
    ```

You can also reinstall the template if it was installed earlier

```bash
dotnet new install --force .
```

To install a template for your device via dotnet:

1. Go to File => New Solution... => More Templates => Install Template...
2. Select folder with .sln file (MicroserviceExtension/src/MicroserviceTemplate)

<br>

---

<br>

## Structure

The project presents several microservices templates. You can find out about them below

### Domain Driven Design

```
Template.Net.Microservice.DDD
├── src
│   ├── Template.Net.Microservice.DDD.Domain
│   │   ├── Entities
│   │   ├── ValueObjects
│   │   ├── Aggregates
│   │   ├── Services
│   │   └── Events
│   ├── Template.Net.Microservice.DDD.Application
│   │   ├── Handlers
│   │   ├── Queries
│   │   ├── Services
│   │   └── Dtos
│   ├── Template.Net.Microservice.DDD.Infrastructure
│   │   ├── Services
│   │   └── ...other
│   └── Template.Net.Microservice.DDD.UI.Api
│       ├── EndPoints
│       ├── Definitions
│       ├── Models
│       └── Startup.cs
```

#### Description of the layers    

1. API Layer:
   - EndPoints: API endpoints for processing HTTP requests and calling application services.
   - Definitions: Simple implementation of "Vertical Slice architecture"
   - Models: Data models used by controllers to receive and send data.

2. Application Layer (Application Layer):

   - Queries: Requests for system data (for example, getting a list of products, order information, creating an order).
   - Handlers: handlers of requests and commands from the application
   - Services: Application services used to coordinate domain operations.
   - Dtos: Data Transfer Objects for transferring data between layers.

3. Domain Layer (Domain Layer):

   - Entities: The main entities of the business logic, such as Product, Order, Customer, Cart, Payment, Shipment.

   - Value Objects: Value objects such as Money, Address.

   - Aggregates: Aggregates that combine entities and value objects into single integral structures.

   - Services: Domain services that implement business logic that does not belong to a specific entity.

   - Events: Domain events that are used for communication within the domain.

4. Infrastructure Layer:

   - Services: Infrastructure services such as services for working with external APIs, file system, etc.
   - ...other: Various interfaces and common code practices

### Three-Tier

```
Template.Net.Microservice.ThreeTier
├── src
│   ├── Template.Net.Microservice.ThreeTier.BL
│   │   ├── Services
│   │   └── ...other
│   ├── Template.Net.Microservice.ThreeTier.DAL
│   │   ├── Database
│   │   ├── Entityes
│   │   ├── ViewModel
│   │   ├── Options
│   │   ├── Mapper
│   │   └── Domain
│   └── Template.Net.Microservice.ThreeTier.PL.Api
│       ├── EndPoints
│       ├── Definitions
│       ├── Models
│       └── Startup.cs
```

1. PL.Api (Presentation Layer. Api):

   - EndPoints: endpoints. A complete description of the domain with viewmodels, requests, handlers and various api

   - Definitions: A simple implementation of the "vertical slice architecture"

2. DAL (Data Access Layer):

   - Database: Database Integration and sidding

   - Entities: Database Entities

   - ViewModels:  Description of both the representation models and the rest of the Dto

   - Options: Models of the Options pattern

   - Mapper: General mapping and applications

   - Domain: General information about the application, such as name, version, authorization

3. BL (Bussines Layer)

   - Services: Infrastructure services such as services for working with external APIs, file system, etc.
   - ...other: Various interfaces and common code practices



<br>

---

<br>

## Configuration

### APPSETTING.JSON & APPSETTING.JSON.DEVELOPER 

Cors permissions. This example is for appsetting.Development.json For regular appsetting.json settings needed by cors

```json
"Cors": {
    "Origins": "*"
}  
```

Connection string for your PostgreSQL database

```json 
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;User Id=postgres;Password=password;Database=Microservice.Identity"
}
```

Configuring logging via serilog. Based on the needs, configure sinks. Note that appsetting.Development.json should have a "MinimumLevel" less than appsetting.json

```json
  "Serilog": {
    "Using": [
      "Serilog.Sinks.Console",
      "Serilog.Sinks.File"
    ],
    "MinimumLevel": {
      "Default": "Debug",
    
    ...
```

The address of the identity server. Must match the address of the application.

```json
"IdentityServerUrl": {
    "Authority": "https://localhost:10001"
}
```

The current server client that will be substituted in swagger.

```json
"CurrentIdentityClient": {
    "Name" : "Microservice.Identity",
    "Id" : "Microservice.Identity-4523f-21321",
    
    ...
  }, 
