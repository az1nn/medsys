#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["medsys-integration-tests/medsys-integration-tests.csproj", "medsys-integration-tests/"]
COPY ["medsys/medsys.csproj", "medsys/"]
RUN dotnet restore "./medsys-integration-tests/./medsys-integration-tests.csproj"
COPY . .
WORKDIR "/src/medsys-integration-tests"
RUN dotnet build "./medsys-integration-tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./medsys-integration-tests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "medsys-integration-tests.dll"]