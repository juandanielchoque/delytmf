# Imagen base de .NET 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagen para construir la app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar todo
COPY . .

# Restaurar paquetes
RUN dotnet restore ./DeliveryApp.API/DeliveryApp.API.csproj

# Publicar
RUN dotnet publish ./DeliveryApp.API/DeliveryApp.API.csproj -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render usa PORT env variable
ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "DeliveryApp.API.dll"]
