#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Categories/Api/Categories.API/Categories.API.csproj", "src/Services/Categories/Api/Categories.API/"]
COPY ["src/Services/Categories/Dal/Categories.DAL/Categories.DAL.csproj", "src/Services/Categories/Dal/Categories.DAL/"]
COPY ["src/Services/Categories/Bll/Categories.BLL/Categories.BLL.csproj", "src/Services/Categories/Bll/Categories.BLL/"]
RUN dotnet restore "src/Services/Categories/Api/Categories.API/Categories.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Categories/Api/Categories.API"
RUN dotnet build "Categories.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Categories.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Categories.API.dll"]