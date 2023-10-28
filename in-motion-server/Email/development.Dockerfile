FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Email/IMS.Email.API/IMS.Email.API.csproj", "./Email/IMS.Email.API/"]
RUN dotnet restore "./Email/IMS.Email.API/IMS.Email.API.csproj"
COPY ./Email ./Email
COPY ./Shared ./Shared
WORKDIR "/src/Email/IMS.Email.API"
RUN dotnet build "IMS.Email.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "IMS.Email.API.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IMS.Email.API.dll"]