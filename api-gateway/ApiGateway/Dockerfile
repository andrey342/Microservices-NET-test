# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

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

COPY ["api-gateway/ApiGateway/ApiGateway.csproj", "ApiGateway/"]
RUN dotnet restore "./ApiGateway/ApiGateway.csproj"
WORKDIR "/src/ApiGateway"
COPY ./api-gateway/ApiGateway ./
RUN dotnet build "./ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]