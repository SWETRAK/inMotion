FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IMS.Auth.API/IMS.Auth.API.csproj", "IMS.Auth.API/"]
RUN dotnet restore "ISM.Auth.API/IMS.Auth.API.csproj"
COPY . .
WORKDIR "/src/IMS.Auth.API"
RUN dotnet build "IMS.Auth.API.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "IMS.Auth.API.csproj" -c Development -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IMS.Auth.App.dll"]