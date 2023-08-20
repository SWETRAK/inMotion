FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["InMotionServer.Auth.App/InMotionServer.Auth.App.csproj", "InMotionServer.Auth.App/"]
RUN dotnet restore "InMotionServer.Auth.App/InMotionServer.Auth.App.csproj"
COPY . .
COPY ../Shared ./Shared
WORKDIR "/src/InMotionServer.Auth.App"
RUN dotnet build "InMotionServer.Auth.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InMotionServer.Auth.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InMotionServer.Auth.App.dll"]
