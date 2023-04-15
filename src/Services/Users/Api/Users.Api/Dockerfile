#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Users/Api/Users.Api/Users.Api.csproj", "src/Services/Users/Api/Users.Api/"]
COPY ["src/Services/Users/Core/Users.Application/Users.Application.csproj", "src/Services/Users/Core/Users.Application/"]
COPY ["src/Services/Users/Core/Users.Domain/Users.Domain.csproj", "src/Services/Users/Core/Users.Domain/"]
COPY ["src/Services/Users/Core/Users.Models/Users.Models.csproj", "src/Services/Users/Core/Users.Models/"]
COPY ["src/Services/Users/Persistence/Users.Persistence/Users.Persistence.csproj", "src/Services/Users/Persistence/Users.Persistence/"]
RUN dotnet restore "src/Services/Users/Api/Users.Api/Users.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Users/Api/Users.Api"
RUN dotnet build "Users.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Users.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Api.dll"]