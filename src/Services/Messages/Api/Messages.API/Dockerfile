#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Messages/Api/Messages.API/Messages.API.csproj", "src/Services/Messages/Api/Messages.API/"]
COPY ["src/Services/Messages/BLL/Messages.BLL/Messages.BLL.csproj", "src/Services/Messages/BLL/Messages.BLL/"]
COPY ["src/Services/Messages/DAL/Messages.DAL/Messages.DAL.csproj", "src/Services/Messages/DAL/Messages.DAL/"]
RUN dotnet restore "src/Services/Messages/Api/Messages.API/Messages.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Messages/Api/Messages.API"
RUN dotnet build "Messages.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messages.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messages.API.dll"]