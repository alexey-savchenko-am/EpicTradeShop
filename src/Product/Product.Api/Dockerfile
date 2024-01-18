FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
    
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /
COPY . .
RUN dotnet restore "src/Product/Product.Api/Product.Api.csproj"
WORKDIR "/src/Product/Product.Api"
RUN dotnet build "Product.Api.csproj" -c Release -o /app
    
FROM build AS publish
WORKDIR "/src/Product/Product.Api"
RUN dotnet publish "Product.Api.csproj" -c Release -o /app
    
FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Product.Api.dll", "--server.urls", "http://+:80"]