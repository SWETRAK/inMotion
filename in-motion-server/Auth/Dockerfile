FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Auth/IMS.Auth.API/IMS.Auth.API.csproj", "./Auth/IMS.Auth.API/"]
RUN dotnet restore "./Auth/IMS.Auth.API/IMS.Auth.API.csproj"
COPY ./Auth ./Auth
COPY ./Shared ./Shared
WORKDIR "/src/Auth/IMS.Auth.API"
RUN dotnet build "IMS.Auth.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IMS.Auth.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IMS.Auth.API.dll"]
