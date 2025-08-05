RssNewsMinimalApi
Minimalistyczne API w .NET 9 służące do pobierania najnowszych wiadomości z kanału RSS Google News.

Funkcje
Endpoint: GET /news
Zwraca listę najnowszych wiadomości w formacie JSON (tytuł, podsumowanie, link, data publikacji).

Wbudowana dokumentacja Swagger
Dostępna pod /swagger po uruchomieniu aplikacji.

Technologia
ASP.NET Core Minimal API (.NET 9)

RSS (SyndicationFeed)

Swagger / Swashbuckle

Jak uruchomić lokalnie
bash
Kopiuj
dotnet run
Swagger dostępny będzie pod:

bash
Kopiuj
http://localhost:5000/swagger
Deployment
Projekt został wdrożony na platformie Render:
👉 https://rssnewsminimalapi.onrender.com
```bash
dotnet run
