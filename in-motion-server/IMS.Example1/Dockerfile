FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IMS.Example1/IMS.Example1.csproj", "IMS.Example1/"]
RUN dotnet restore "IMS.Example1/IMS.Example1.csproj"
COPY . .
WORKDIR "/src/IMS.Example1"
RUN dotnet build "IMS.Example1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IMS.Example1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IMS.Example1.dll"]
