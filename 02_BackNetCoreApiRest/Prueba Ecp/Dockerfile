FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["testApi/testEcpApi.csproj", "testApi/"]
RUN dotnet restore "testApi/testEcpApi.csproj"
COPY . .
WORKDIR "/src/testApi"
RUN dotnet build "testEcpApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "testEcpApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "testEcpApi.dll"]