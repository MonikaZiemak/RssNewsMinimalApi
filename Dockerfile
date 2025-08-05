# Etap 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Kopiuj pliki projektu i przywróć zależności
COPY *.csproj ./
RUN dotnet restore

# Skopiuj resztę i zbuduj
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Etap 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Skopiuj opublikowaną aplikację
COPY --from=build /app/out ./

# Render wymaga nasłuchiwania na $PORT
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 80

ENTRYPOINT ["dotnet", "RssNewsMinimalApi.dll"]
