

# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /

# Modificar NuGet.Config
RUN mkdir -p /root/.nuget/NuGet && \
    echo '<?xml version="1.0" encoding="utf-8"?>' > /root/.nuget/NuGet/NuGet.Config && \
    echo '<configuration>' >> /root/.nuget/NuGet/NuGet.Config && \
    echo '  <packageSources>' >> /root/.nuget/NuGet/NuGet.Config && \
    echo '    <add key="nexus" value="https://nexus.grupoclece.com/repository/nuget.org-proxy/index.json" />' >> /root/.nuget/NuGet/NuGet.Config && \
    echo '  </packageSources>' >> /root/.nuget/NuGet/NuGet.Config && \
    echo '  <packageSourceCredentials>' >> /root/.nuget/NuGet/NuGet.Config && \
    echo '    <nexus>' >> /root/.nuget/NuGet/NuGet.Config && \
    echo '      <add key="Username" value="docker-group-read" />' >> /root/.nuget/NuGet/NuGet.Config && \
    echo '      <add key="ClearTextPassword" value="DockeR123#" />' >> /root/.nuget/NuGet/NuGet.Config && \ 
    echo '    </nexus>' >> /root/.nuget/NuGet/NuGet.Config && \
    echo '  </packageSourceCredentials>' >> /root/.nuget/NuGet/NuGet.Config && \
    echo '</configuration>' >> /root/.nuget/NuGet/NuGet.Config

COPY ["src/UserManagement/UserManagement.API/UserManagement.API.csproj", "src/UserManagement/UserManagement.API/"]
COPY ["src/Shared/EventBusKafka/EventBusKafka.csproj", "src/Shared/EventBusKafka/"]
COPY ["src/Shared/EventBus/EventBus.csproj", "src/Shared/EventBus/"]
COPY ["src/Shared/IntegrationEventLog/IntegrationEventLog.csproj", "src/Shared/IntegrationEventLog/"]
COPY ["src/UserManagement/UserManagement.Domain/UserManagement.Domain.csproj", "src/UserManagement/UserManagement.Domain/"]
COPY ["src/UserManagement/UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "src/UserManagement/UserManagement.Infrastructure/"]
RUN dotnet restore "./src/UserManagement/UserManagement.API/UserManagement.API.csproj"
COPY . .
WORKDIR "/src/UserManagement/UserManagement.API"
RUN dotnet build "./UserManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.API.dll"]