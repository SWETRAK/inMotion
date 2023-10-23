FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Friends/IMS.Friends.API/IMS.Friends.API.csproj", "Friends/IMS.Friends.API/"]
RUN dotnet restore "Friends/IMS.Friends.API/IMS.Friends.API.csproj"
COPY ./Friends ./Friends
COPY ./Shared ./Shared
WORKDIR "/src/Friends/IMS.Friends.API"
RUN dotnet build "IMS.Friends.API.csproj" -c DockerProduction -o /app/build

FROM build AS publish
RUN dotnet publish "IMS.Friends.API.csproj" -c DockerProduction -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IMS.Friends.API.dll"]
