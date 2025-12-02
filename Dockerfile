# ================================
# 1) Build stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar la solución y los proyectos
COPY . .

# Restaurar dependencias
RUN dotnet restore ./delytmf.sln

# Compilar en Release
RUN dotnet publish ./DeliveryApp.API/DeliveryApp.API.csproj -c Release -o /app/publish

# ================================
# 2) Run stage
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://0.0.0.0:5000

ENTRYPOINT ["dotnet", "DeliveryApp.API.dll"]
