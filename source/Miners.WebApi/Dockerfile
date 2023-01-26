#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["source/Miners.WebApi/Miners.WebApi.csproj", "source/Miners.WebApi/"]
COPY ["source/Miners.Application/Miners.Application.csproj", "source/Miners.Application/"]
COPY ["source/Miners.Domain/Miners.Domain.csproj", "source/Miners.Domain/"]
COPY ["source/Miners.Infrastructure/Miners.Infrastructure.csproj", "source/Miners.Infrastructure/"]
RUN dotnet restore "source/Miners.WebApi/Miners.WebApi.csproj"
COPY . .
WORKDIR "/src/source/Miners.WebApi"
RUN dotnet build "Miners.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Miners.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Miners.WebApi.dll"]