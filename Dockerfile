#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelloWorldServer.csproj", "."]
RUN dotnet restore "./HelloWorldServer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HelloWorldServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloWorldServer.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get -y update; apt-get -y install curl
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloWorldServer.dll"]