FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProsperousUniverse.API/ProsperousUniverse.API.csproj", "ProsperousUniverse.API/"]
COPY ["socket.io-client-csharp/src/SocketIOClient/SocketIOClient.csproj", "SocketIOClient/"]
RUN dotnet restore "ProsperousUniverse.API/ProsperousUniverse.API.csproj"
COPY . .
WORKDIR "/src/ProsperousUniverse.API"
RUN dotnet build "ProsperousUniverse.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProsperousUniverse.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProsperousUniverse.API.dll"]
