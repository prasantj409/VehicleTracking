#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/VehicleTracking.Services.RecordDevicePosition/VehicleTracking.Services.RecordDevicePosition.csproj", "src/VehicleTracking.Services.RecordDevicePosition/"]
COPY ["src/Application/VehicleTracking.Services/VehicleTracking.Service.csproj", "src/Application/VehicleTracking.Services/"]
COPY ["src/Domain/VehicleTracking.Domain/VehicleTracking.Domain.csproj", "src/Domain/VehicleTracking.Domain/"]
COPY ["src/Infrastructure/VehicleTracking.Repository/VehicleTracking.Repository.csproj", "src/Infrastructure/VehicleTracking.Repository/"]
COPY ["src/Infrastructure/Persistence/VehicleTracking.DB/VehicleTracking.DB.csproj", "src/Infrastructure/Persistence/VehicleTracking.DB/"]
RUN dotnet restore "src/VehicleTracking.Services.RecordDevicePosition/VehicleTracking.Services.RecordDevicePosition.csproj"
COPY . .
WORKDIR "/src/src/VehicleTracking.Services.RecordDevicePosition"
RUN dotnet build "VehicleTracking.Services.RecordDevicePosition.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VehicleTracking.Services.RecordDevicePosition.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehicleTracking.Services.RecordDevicePosition.dll"]