# Games API

Backend for managing game configurations. Built with ASP.NET Core Minimal API + SQLite.

## What it does

Serves game configs (like Magic Memory) to frontend clients and provides an admin panel for editing them.

Each game has common fields (`slug`, `genre`, `description`, `imageUrl`) and a flexible JSON `config` for game-specific parameters.

## API Endpoints

### Public

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/games` | All games as `{ slug: gameData }` object |
| GET | `/api/games/{slug}` | Single game by slug |

### Admin

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/admin/games` | List all games (flat array) |
| POST | `/api/admin/games` | Create a new game |
| PUT | `/api/admin/games/{id}` | Update game by id |
| DELETE | `/api/admin/games/{id}` | Delete game (system games protected) |
| POST | `/api/admin/upload` | Upload an image (returns URL) |

### Admin Panel

Available at `http://localhost:5000/admin/index.html` (or just `/`).

## Run

```bash
dotnet ef database update   # apply migrations
dotnet run                  # starts on http://localhost:5000
```

## Tech Stack

- .NET 10, Minimal API
- SQLite + EF Core
- Static files for admin UI
