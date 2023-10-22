FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ImsGateway/ImsGateway.csproj", "ImsGateway/"]
RUN dotnet restore "ImsGateway/ImsGateway.csproj"
COPY . .
WORKDIR "/src/ImsGateway"
RUN dotnet build "ImsGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImsGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImsGateway.dll"]
