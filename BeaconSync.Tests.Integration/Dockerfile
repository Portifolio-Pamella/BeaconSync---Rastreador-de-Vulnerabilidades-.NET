# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os arquivos de solução e projetos
COPY ["BeaconSync.API/BeaconSync.API.csproj", "BeaconSync.API/"]
COPY ["BeaconSync.Application/BeaconSync.Application.csproj", "BeaconSync.Application/"]
COPY ["BeaconSync.Domain/BeaconSync.Domain.csproj", "BeaconSync.Domain/"]
COPY ["BeaconSync.Infrastructure/BeaconSync.Infrastructure.csproj", "BeaconSync.Infrastructure/"]
COPY ["BeaconSync.Tests.Unit/BeaconSync.Tests.Unit.csproj", "BeaconSync.Tests.Unit/"]
COPY ["BeaconSync.Tests.Integration/BeaconSync.Tests.Integration.csproj", "BeaconSync.Tests.Integration/"]

# Restaura as dependências
RUN dotnet restore "BeaconSync.API/BeaconSync.API.csproj"

# Copia todo o código fonte restante
COPY . .
WORKDIR "/src/BeaconSync.API"
RUN dotnet build "BeaconSync.API.csproj" -c Release -o /app/build

# Estágio de Publicação
FROM build AS publish
RUN dotnet publish "BeaconSync.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define a porta padrão exigida pelo Render
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 8080

ENTRYPOINT ["dotnet", "BeaconSync.API.dll"]