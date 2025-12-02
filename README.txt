DeliveryAppMySqlNet9 - API de Delivery (.NET 9 + MySQL + Arquitectura Hexagonal/Clean)

DB:
- MySQL localhost:3306
- user: root
- password: danny
- database: DeliveryAppDb (se crea por migraciones)

Pasos:

1. Crear la base vacía (opcional, EF puede crearla):
   CREATE DATABASE IF NOT EXISTS DeliveryAppDb;

2. En terminal:
   dotnet restore

3. Instalar dotnet-ef si no lo tienes:
   dotnet tool install --global dotnet-ef

4. Crear migraciones y actualizar DB:
   cd DeliveryApp.Infrastructure
   dotnet ef migrations add InitialCreate -s ../DeliveryApp.API/DeliveryApp.API.csproj
   dotnet ef database update -s ../DeliveryApp.API/DeliveryApp.API.csproj

5. Ejecutar API:
   dotnet run --project DeliveryApp.API/DeliveryApp.API.csproj

6. Abrir Swagger:
   https://localhost:5001/swagger

Rúbrica:
- Arquitectura hexagonal/clean:
  - Domain: entidades y lógica pura.
  - Application: interfaces (puertos) + servicios/casos de uso.
  - Infrastructure: adaptadores (repositorios, EF, JWT).
  - API: adaptador primario (controllers).
- Patrón Repository + UnitOfWork implementados.
- Middlewares:
  - ExceptionHandlingMiddleware.
  - RequestLoggingMiddleware.
- Controllers: delegan lógica a servicios, validan ModelState.
- Reportes:
  - /api/reports/orders/csv genera CSV de órdenes (solo Admin).
