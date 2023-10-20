#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Order.Query.Api/Order.Query.Api.csproj", "Order.Query.Api/"]
COPY ["Order.Query.Domain/Order.Query.Domain.csproj", "Order.Query.Domain/"]
COPY ["Order.Common/Order.Common.csproj", "Order.Common/"]
COPY ["CQRS.Core/CQRS.Core.csproj", "CQRS.Core/"]
COPY ["Order.Query.Infrastructure/Order.Query.Infrastructure.csproj", "Order.Query.Infrastructure/"]
RUN dotnet restore "Order.Query.Api/Order.Query.Api.csproj"
COPY . .
WORKDIR "/src/Order.Query.Api"
RUN dotnet build "Order.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Query.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Query.Api.dll"]