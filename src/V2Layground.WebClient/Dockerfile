FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["V2Layground.WebClient/V2Layground.WebClient.csproj", "V2Layground.WebClient/"]
RUN dotnet restore "V2Layground.WebClient/V2Layground.WebClient.csproj"
COPY . .
WORKDIR "/src/V2Layground.WebClient"
RUN dotnet build "V2Layground.WebClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "V2Layground.WebClient.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "V2Layground.WebClient.dll", "--urls", "http://*;https://*"]
