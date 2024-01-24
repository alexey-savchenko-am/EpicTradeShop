FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
    
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /
COPY . .
RUN dotnet restore "src/Stock/Stock.Api/Stock.Api.csproj"
WORKDIR "/src/Stock/Stock.Api"
RUN dotnet build "Stock.Api.csproj" -c Release -o /app
    
FROM build AS publish
WORKDIR "/src/Stock/Stock.Api"
RUN dotnet publish "Stock.Api.csproj" -c Release -o /app
    
FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Stock.Api.dll", "--server.urls", "http://+:80"]