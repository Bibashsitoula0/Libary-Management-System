#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BookHive/BookHive.csproj", "BookHive/"]
COPY ["BookHive.Dal/BookHive.Dal.csproj", "BookHive.Dal/"]
COPY ["BookiHive.Model/BookiHive.Model.csproj", "BookiHive.Model/"]
COPY ["Bookhive.Service/Bookhive.Service.csproj", "Bookhive.Service/"]
RUN dotnet restore "./BookHive/./BookHive.csproj"
COPY . .
WORKDIR "/src/BookHive"
RUN dotnet build "./BookHive.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookHive.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookHive.dll"]