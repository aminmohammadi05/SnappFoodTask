#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5555

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Order.Command.Api/Order.Command.Api.csproj", "Order.Command.Api/"]
COPY ["Order.Command.Domain/Order.Command.Domain.csproj", "Order.Command.Domain/"]
COPY ["Order.Common/Order.Common.csproj", "Order.Common/"]
COPY ["CQRS.Core/CQRS.Core.csproj", "CQRS.Core/"]
COPY ["Order.Command.Infrastructure/Order.Command.Infrastructure.csproj", "Order.Command.Infrastructure/"]
RUN dotnet restore "Order.Command.Api/Order.Command.Api.csproj"
COPY . .
WORKDIR "/src/Order.Command.Api"
RUN dotnet build "Order.Command.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Command.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Command.Api.dll"]