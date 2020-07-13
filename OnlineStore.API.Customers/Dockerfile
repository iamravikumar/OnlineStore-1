#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["OnlineStore.API.Customers/OnlineStore.API.Customers.csproj", "OnlineStore.API.Customers/"]
RUN dotnet restore "OnlineStore.API.Customers/OnlineStore.API.Customers.csproj"
COPY . .
WORKDIR "/src/OnlineStore.API.Customers"
RUN dotnet build "OnlineStore.API.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineStore.API.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStore.API.Customers.dll"]
