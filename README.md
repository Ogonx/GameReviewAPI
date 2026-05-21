# GameReviewAPI

A RESTful Web API built with ASP.NET Core .NET 9, Entity Framework Core, and Azure SQL. Allows users to search for games, import them from the RAWG database, and submit reviews with aggregated scoring.

## Live API
Swagger UI: https://gamereviewapi-production.up.railway.app/swagger

## Tech Stack
- ASP.NET Core Web API (.NET 9)
- Entity Framework Core
- Azure SQL Database
- RAWG Video Games API
- Deployed on Railway

## Features
- Search games via RAWG API (500,000+ games)
- Import games into local database
- Submit, retrieve, and delete reviews
- Aggregate average score per game
- Filter reviews by game

## Endpoints

### Games
- `GET /api/Games` — Get all games
- `GET /api/Games/{id}` — Get game by ID
- `GET /api/Games/{id}/averagescore` — Get average review score for a game
- `POST /api/Games` — Add a game manually
- `DELETE /api/Games/{id}` — Delete a game

### Reviews
- `GET /api/Reviews` — Get all reviews
- `GET /api/Reviews/{id}` — Get review by ID
- `GET /api/Reviews/game/{gameId}` — Get all reviews for a game
- `POST /api/Reviews` — Submit a review
- `DELETE /api/Reviews/{id}` — Delete a review

### RAWG
- `GET /api/Rawg/search?query={name}` — Search games on RAWG
- `POST /api/Rawg/import/{rawgId}` — Import a game from RAWG into the database

## Running Locally
1. Clone the repo
2. Add your RAWG API key via user secrets:
   `dotnet user-secrets set "Rawg:ApiKey" "your-key-here"`
3. Update the connection string in `appsettings.json`
4. Run `Update-Database` in Package Manager Console
5. Hit F5