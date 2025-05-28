# Étape 1 : build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie les fichiers solution et tous les fichiers .csproj dans leurs dossiers respectifs
COPY *.sln .
COPY Catmash.Api/*.csproj ./Catmash.Api/
COPY Catmash.Domain/*.csproj ./Catmash.Domain/
COPY Catmash.Application/*.csproj ./Catmash.Application/
COPY Catmash.Infrastructure/*.csproj ./Catmash.Infrastructure/
COPY Catmash.Tests/*.csproj ./Catmash.Tests/

# Restore les dépendances
RUN dotnet restore

# Copie le reste du code
COPY . .

# Build et publish le projet
RUN dotnet publish Catmash.Api/Catmash.Api.csproj -c Release -o /app/publish

# Étape 2 : image finale runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Démarre ton app ok
ENTRYPOINT ["dotnet", "Catmash.Api.dll"]