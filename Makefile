# Don't change
SRC_DIR := src
TEST_DIR := test
SCRIPTS_DIR := scripts
DOCS_DIR := docs
BANK_API_DIR := $(SRC_DIR)/Payment.Bank.Api
FROM_REPO := mcr.microsoft.com/dotnet
DOTNET_SDK_IMAGE := sdk:8.0-jammy
ASPNET_RUNTIME_IMAGE := aspnet:8.0-jammy-chiseled
RUNTIME_IDENTIFIER := linux-x64

# Used by `image`, `push` & `deploy` override as required
IMAGE_REG ?= ghcr.io
IMAGE_REPO ?= salzaki/bank
IMAGE_TAG ?= latest
IMAGE_PREFIX := $(IMAGE_REG)/$(IMAGE_REPO)
IMAGE_LIST := bank-api
VERSION ?= 1.0.0
BUILD_INFO ?= "Local makefile build"
.EXPORT_ALL_VARIABLES:
.PHONY: help check docker-lint docker-start docker-stop lint lint-fix clean install-docs build-docs serve-docs run-docs clean-docs build-docker-docs serve-docker-docs clean-certs
.DEFAULT_GOAL := help

help:  ## 💬 This help message
	@$(SCRIPTS_DIR)/Makefile_help.sh ./Makefile

check: ## 🔍 Checks installed dependencies on local machine
	@echo "🔍 Checking installed dependencies..."
	@echo "dotnet Version -" $(shell dotnet --version)
	@echo "node Version -" $(shell node --version)
	@echo $(shell git --version)
	@echo $(shell docker --version)
	@echo $(shell docker-compose --version)
	@echo "✅ Done checking installed dependencies."
#########################################################
# docker targets
#########################################################
docker-lint: ## 🐳 Lints Dockerfile
	@echo "🔍 Linting Dockerfile..."
	@cd $(BANK_API_DIR) && \
	docker run --rm -i ghcr.io/hadolint/hadolint:latest < Dockerfile
	@echo "✅ Done linting Dockerfile."

docker-build: ## 🏃 Builds bank.api container using Docker compose
	@echo "🏃‍ Building bank-api locally using Dotnet CLI..."
	docker-compose up --build --remove-orphans
	@echo "✅ Done building bank-api locally using Dotnet CLI."

docker-start: ## 🏃 Stars bank.api container using Docker compose
	@echo "🏃‍ Staring bank-api locally using Dotnet CLI..."
	docker compose -f docker-compose.yml up -d
	@echo "✅ Done staring bank-api locally using Dotnet CLI."

docker-stop: ## 🏃 Stops bank.api container using Docker compose
	@echo "🏃‍ Stopping bank-api locally using Dotnet CLI..."
	docker compose -f docker-compose.yml down
	@echo "✅ Done stopping bank-api locally using Dotnet CLI."

lint: ## 🔎 Checks for linting and formatting errors in code
	@echo "🔎 Checking for linting and formatting errors in bank api..."
	@cd $(BANK_API_DIR) && \
	dotnet format --verbosity detailed --verify-no-changes Payment.Bank.Api.csproj
	@echo "✅ Done checking for linting and formatting errors in bank api."

lint-fix: ## 🔧 Lints & formats, fixes errors and modifies code
	@echo "🔧 Linting and formatting, fixing errors, and modifying code..."
	@cd $(BANK_API_DIR) && \
	dotnet format --verbosity detailed Payment.Bank.Api.csproj
	@echo "✅ Done linting and formatting, fixing errors, and modifying code."

clean: ## 🧹 Cleans up project
	@echo "### WARNING! 🧹 Going to delete all binaries in projects... 😲"
	rm -rf $(BANK_API_DIR)/bin
	rm -rf $(BANK_API_DIR)/obj
	@echo "✅ Done deleting all binaries in bank-api."
#########################################################
# docs targets
#########################################################
install-docs: ## 🛠️ Installs necessary dependencies to build docs in Ruby
	@echo "🛠️ Installing dependencies..."
	@cd $(DOCS_DIR) && \
	bundle install
	@echo "✅ Done Installing dependencies."

build-docs: ## 🔨 Builds docs on local machine
	@echo "🔨️ Building docs..."
	@cd $(DOCS_DIR) && \
	bundle install; bundle exec jekyll build --drafts
	@echo "✅ Done building docs."

serve-docs: ## 🏃️ Runs project docs (this does not listen for changes)
	@echo "🏃 Running project docs"
	@cd $(DOCS_DIR) && \
	bundle exec jekyll serve
	@echo "✅ Done running project docs."

run-docs: ## 🤖 Runs project docs (this listens for changes)
	@echo "🤖️ Runs project docs..."
	@cd $(DOCS_DIR) && \
	bundle install; bundle exec jekyll serve --drafts --incremental --config _config.yml
	@echo "✅ Done running project docs." 

clean-docs:  ## 🧹 Cleans docs site
	@echo "🔨️ Cleaning docs site..."
	@cd $(DOCS_DIR) && \
	bundle exec jekyll clean
	@echo "✅ Done cleaning docs site."

build-docker-docs:
	docker run --rm \
		--volume="$(PWD)/docs/:/srv/jekyll" \
		--volume="$(PWD)/docs/vendor/bundle:/usr/local/bundle" \
		-p 4000:4000 \
		-it jekyll/jekyll:latest \
		jekyll build --trace

serve-docker-docs:
	docker run --rm \
		--volume="$(PWD)/docs/:/srv/jekyll" \
		--volume="$(PWD)/docs/vendor/bundle:/usr/local/bundle" \
		-p 4000:4000 \
		-it jekyll/jekyll \
		jekyll serve
#########################################################
# certs targets
######################################################### 
check-certs: ## 🔍 Checks development certs
	@echo "🔍 Checking development certs..."
	dotnet dev-certs https --check --trust
	@echo "✅ Done checking development certs."

clean-certs: ## 🤖 Cleans up development certs
	@echo "🤖️ Cleaning development certs..."
	dotnet dev-certs https --clean
	@echo "✅ Done cleaning development certs." 

install-certs: ## 🔐 Installs development certs
	@echo "🔐️ Installing development certs..."
	dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p devcertpassword --trust
	@echo "✅ Done installing development certs."