# Étape 1 : build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie les fichiers solution et projet
COPY *.sln .
COPY Catmash.Api/*.csproj ./Catmash.Api/

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

# Démarre ton app
ENTRYPOINT ["dotnet", "Catmash.Api.dll"]
