FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["V2Layground.Identity/V2Layground.Identity.csproj", "V2Layground.Identity/"]
RUN dotnet restore "V2Layground.Identity/V2Layground.Identity.csproj"
COPY . .
WORKDIR "/src/V2Layground.Identity"
RUN dotnet build "V2Layground.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "V2Layground.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "V2Layground.Identity.dll", "--urls", "http://*;https://*"]
