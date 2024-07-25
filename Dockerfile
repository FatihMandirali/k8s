#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["K8S.API/K8S.API.csproj", "K8S.API/"]
RUN dotnet restore "K8S.API/K8S.API.csproj"
COPY . .
WORKDIR "/src/K8S.API"
RUN dotnet build "K8S.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "K8S.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "K8S.API.dll"]
