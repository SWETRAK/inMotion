FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./User/IMS.User.API/IMS.User.API.csproj", "./User/IMS.User.API/"]
RUN dotnet restore "./User/IMS.User.API/IMS.User.API.csproj"
COPY ./User ./User
COPY ./Shared ./Shared
WORKDIR "/src/User/IMS.User.API"
RUN dotnet build "IMS.User.API.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "IMS.User.API.csproj" -c Development -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IMS.User.API.dll"]
