FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["V2Layground.ResourceApi/V2Layground.ResourceApi.csproj", "V2Layground.ResourceApi/"]
RUN dotnet restore "V2Layground.ResourceApi/V2Layground.ResourceApi.csproj"
COPY . .
WORKDIR "/src/V2Layground.ResourceApi"
RUN dotnet build "V2Layground.ResourceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "V2Layground.ResourceApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "V2Layground.ResourceApi.dll", "--urls", "http://*;https://*"]
