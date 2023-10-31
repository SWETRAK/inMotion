FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Post/IMS.Post.API/IMS.Post.API.csproj", "Post/IMS.Post.API/"]
RUN dotnet restore "Post/IMS.Post.API/IMS.Post.API.csproj"
COPY ./Post ./Post
COPY ./Shared ./Shared
WORKDIR "/src/Post/IMS.Post.API"
RUN dotnet build "IMS.Post.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IMS.Post.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IMS.Post.API.dll"]
