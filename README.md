# Anazon

An e-commerce backend API built with .NET 9, organised as vertical slices rather than layers.

## Stack

.NET 9 · Carter · MediatR · FluentValidation · EF Core 9 · MySQL · JWT bearer auth · Swagger

## Architecture

Each feature owns its endpoint, handler, validator and DTOs in a single folder under `Src/Features/`, so a change to one feature touches one directory:

```
Src/
  Features/        Attribute · AttributeValue · Auth · Brand · Category · Product · Tags
  Behaviors/       MediatR pipeline behaviours (validation, cross-cutting concerns)
  Shared/          Authorization, contracts, DB helpers, services
  Database/        DbContext and entity configuration
  ExceptionHandlers/
Migrations/        EF Core migrations
IoC.cs             Service registration
Middlewares.cs     Pipeline setup
```

Endpoints are Carter modules, requests go through MediatR, and validation runs as a pipeline behaviour rather than inside handlers.

## Running it

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Set the connection string and JWT settings in `appsettings.Development.json`. Swagger UI is served at `/swagger` in development.

## Status

Personal project, actively built. Product variants and attribute-value CRUD are still in progress.
