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
    cd OpenIddictService/Service
    ```
2. Perform the installation
    ```bash
    dotnet new --install .
    ```

To install a template for your device via dotnet:

1. Go to File => New Solution... => More Templates => Install Template...
2. Select folder with .sln file (OpenIddict/Service/OpenIddictServiceTemplate)

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

### IDENTITYSETTING.JSON

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
