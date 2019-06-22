FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["SimonsSearch.API/SimonsSearch.API.csproj", "SimonsSearch.API/"]
COPY ["SimonsSearch.Service/SimonsSearch.Service.csproj", "SimonsSearch.Service/"]
RUN dotnet restore "SimonsSearch.API/SimonsSearch.API.csproj"
COPY . .
WORKDIR "/src/SimonsSearch.API"
RUN dotnet build "SimonsSearch.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SimonsSearch.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SimonsSearch.API.dll"]