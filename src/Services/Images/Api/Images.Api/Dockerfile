#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Images/Api/Images.Api/Images.Api.csproj", "src/Services/Images/Api/Images.Api/"]
COPY ["src/Services/Images/Core/Images.Core/Images.Core.csproj", "src/Services/Images/Core/Images.Core/"]
COPY ["src/Services/Images/Infrastructure/Images.Infrastructure/Images.Infrastructure.csproj", "src/Services/Images/Infrastructure/Images.Infrastructure/"]
RUN dotnet restore "src/Services/Images/Api/Images.Api/Images.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Images/Api/Images.Api"
RUN dotnet build "Images.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Images.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Images.Api.dll"]