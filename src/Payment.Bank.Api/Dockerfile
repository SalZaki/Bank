# ===================================================================================
# === Stage 1: Build the Go service code into 'server' exe ==========================
# ===================================================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
# ================================================================================================
# === Stage 2: Get server binary into a lightweight container ====================================
# ================================================================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Payment.Bank.Api/Payment.Bank.Api.csproj", "src/Payment.Bank.Api/"]
COPY ["src/Payment.Bank.Common/Payment.Bank.Common.csproj", "src/Payment.Bank.Common/"]
RUN dotnet restore "src/Payment.Bank.Api/Payment.Bank.Api.csproj"
COPY . .
WORKDIR "/src/src/Payment.Bank.Api"
RUN dotnet build "Payment.Bank.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
# ================================================================================================
# === Stage 3: Get server binary into a lightweight container ====================================
# ================================================================================================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Payment.Bank.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
# ================================================================================================
# === Stage 4: Get server binary into a lightweight container ====================================
# ================================================================================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT Docker

ENTRYPOINT ["dotnet", "Payment.Bank.Api.dll"]
