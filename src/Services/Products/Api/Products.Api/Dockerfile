#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Products/Api/Products.Api/Products.Api.csproj", "src/Services/Products/Api/Products.Api/"]
COPY ["src/Services/Products/Core/Products.Application/Products.Application.csproj", "src/Services/Products/Core/Products.Application/"]
COPY ["src/Services/Products/Core/Products.Domain/Products.Domain.csproj", "src/Services/Products/Core/Products.Domain/"]
COPY ["src/Services/Products/Core/Products.Exceptions/Products.Exceptions.csproj", "src/Services/Products/Core/Products.Exceptions/"]
COPY ["src/Services/Products/Core/Products.Models/Products.Models.csproj", "src/Services/Products/Core/Products.Models/"]
COPY ["src/Services/Products/Persistence/Products.Persistence/Products.Persistence.csproj", "src/Services/Products/Persistence/Products.Persistence/"]
RUN dotnet restore "src/Services/Products/Api/Products.Api/Products.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Products/Api/Products.Api"
RUN dotnet build "Products.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Products.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Products.Api.dll"]