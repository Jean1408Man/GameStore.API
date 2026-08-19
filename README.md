# GameStore

API de una tienda de videojuegos creada como proyecto de aprendizaje con ASP.NET Core, Entity Framework Core y SQLite.

## Requisitos

- .NET SDK 10

## Ejecutar el proyecto

```bash
dotnet restore
dotnet run --project GameStore.API
```

La base de datos SQLite es local y no se incluye en Git. Para crearla a partir de las migraciones:

```bash
dotnet ef database update --project GameStore.API
```

Los endpoints pueden probarse desde `GameStore.API/games.http`.

