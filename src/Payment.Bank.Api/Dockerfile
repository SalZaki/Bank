ARG FROM_REPO=mcr.microsoft.com/dotnet
ARG DOTNET_SDK_IMAGE=sdk:8.0-jammy
ARG ASPNET_RUNTIME_IMAGE=aspnet:8.0-jammy-chiseled
# hadolint ignore=DL3006
FROM --platform=$BUILDPLATFORM ${FROM_REPO}/${DOTNET_SDK_IMAGE} as build-image
# hadolint ignore=DL3006
FROM --platform=$BUILDPLATFORM ${FROM_REPO}/${ASPNET_RUNTIME_IMAGE} as runtime-image
## ================================================================================================
## Stage 1: 🚧 Building secure and lightweight image
## ================================================================================================
FROM build-image AS build
# Set default build info
ARG BUILD_INFO="Not provided"
# Allow setting the assembly version and build configuration from the build command
ARG RUNTIME_IDENTIFIER=linux-x64
ARG BUILD_CONFIGURATION=Release
ARG VERSION=0.0.0
# Set working directory and copy project files
WORKDIR /src
COPY ["src/Payment.Bank.Api/Payment.Bank.Api.csproj", "Payment.Bank.Api/"]
COPY ["src/Payment.Bank.Application/Payment.Bank.Application.csproj", "Payment.Bank.Application/"]
COPY ["src/Payment.Bank.Common/Payment.Bank.Common.csproj", "Payment.Bank.Common/"]
COPY ["src/Payment.Bank.Domain/Payment.Bank.Domain.csproj", "Payment.Bank.Domain/"]
COPY ["src/Payment.Bank.Infrastructure/Payment.Bank.Infrastructure.csproj", "Payment.Bank.Infrastructure/"]
COPY ./nuget.config ./
COPY *.props ./
RUN echo "🔍 Restoring NuGet packages..."
RUN dotnet restore "Payment.Bank.Api/Payment.Bank.Api.csproj"  \
    -r $RUNTIME_IDENTIFIER

COPY /src .
# Build the project
WORKDIR /src/Payment.Bank.Api
RUN echo "🚧 Building the project..." \
RUN dotnet build "Payment.Bank.Api.csproj"  \
    -c $BUILD_CONFIGURATION  \
    -o /app/build  \
    -p:Version=$VERSION
# ================================================================================================
# === Stage 2: 📦 Publish a server binary into a lightweight container =========================
# ================================================================================================
FROM build AS publish
ARG BUILD_CONFIGURATION
# Publish a self-contained assembly so we can use a slimmer runtime image
# hadolint ignore=SC2086
RUN echo "📦 Publishing the project..."
RUN dotnet publish "Payment.Bank.Api.csproj" \
    --configuration $BUILD_CONFIGURATION \
    --no-self-contained \
    --runtime $RUNTIME_IDENTIFIER \
    --output /app/publish \
    -p:Version=$VERSION \
    -p:TreatWarningsAsErrors=true
### ================================================================================================
### === Stage 3: Build stage with a secure and lightweight runtime image ===========================
### ================================================================================================
FROM runtime-image AS runtime
# Set maintainer and website labels
LABEL maintainer="salzaki@hotmail.com"
LABEL org.website="https://salzaki.github.io/Bank/"
# Set working directory and expose ports
WORKDIR /app
EXPOSE 80
EXPOSE 443
# Set user context for security
USER app
### ================================================================================================
### === Stage 4: Get server binary into a lightweight container ====================================
### ================================================================================================
FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.Bank.Api.dll"]