# GameReviewAPI

REST API for a personal game review platform. Search and import games from RAWG, submit reviews, and get aggregated scores.

## Links
- Swagger: https://gamereviewapi-production.up.railway.app/swagger
- Frontend: https://game-review-ui.vercel.app

## Stack
ASP.NET Core Web API (.NET 9), Entity Framework Core, PostgreSQL, RAWG API, deployed on Railway

## Endpoints

`GET /api/Rawg/search?query=` — search RAWG  
`POST /api/Rawg/import/{rawgId}` — import a game to the database  
`GET /api/Games` — all games  
`GET /api/Games/{id}/averagescore` — aggregated score for a game  
`GET /api/Reviews/game/{gameId}` — all reviews for a game  
`POST /api/Reviews` — submit a review  

## Running locally
1. Clone the repo
2. Set your RAWG key: `dotnet user-secrets set "Rawg:ApiKey" "your-key"`
3. Update the connection string in `appsettings.json`
4. Run `Update-Database` in Package Manager Console
5. F5
