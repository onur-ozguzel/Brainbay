# Brainbay

This is a POC (Proof of Concept) project. Below you can find the details of its purpose and structure.

Throughout the codebase, you'll find comments starting with `NOTE:` — these are added to explain specific implementation decisions or trade-offs I made during development.

I chose PostgreSQL because it's free and easy to containerize. For a small POC like this, it might be overkill — SQLite could have been a simpler alternative.

# Rick and Morty Characters WebApp 🛸

This project is a Razor Pages web application that displays characters from the **Rick and Morty** universe, with features like:

- Pagination
- Location-based filtering
- Output caching for improved performance
- Integration with an external data source via background fetcher service

---

## 🧱 Projects in the Solution

### 📦 `RickAndMortyDataFetcher`

A "fire-and-forget" console service that:

- Fetches character data from the external Rick and Morty API
- Normalizes and stores it in a PostgreSQL database
- Is intended to run once to initialize (or refresh) the character data
- ⚠️ **Note:** It clears the existing database on each run

### 🖥️ `RickAndMortyWebApp`

A Razor Pages web application that:

- Displays character lists with pagination
- Supports filtering by character location
- Uses OutputCache to improve response time for repeated requests
- Includes example **xUnit unit tests** and **integration tests**

---

## 🐳 Running the Project with Docker

To run everything using Docker, navigate to the **root folder** and run:

```powershell
docker compose up --build -d
```
This command will build and start the following containers:

- `postgres-1`: PostgreSQL database, exposed on port 5432
- `data-fetcher-svc-1`: Runs once to fetch and seed character data
- `web-app-svc-1`: Razor Pages web app, accessible at http://localhost:8080

💡 The `data-fetcher-svc` will exit automatically after completing the initial database setup.

## Running the Project locally

If you prefer to run the applications locally (e.g., through Visual Studio) while using Docker for the database only:

1. Start PostgreSQL via Docker from the root folder:
```powershell
docker compose up postgres -d
```
This will start only the database container, mapped to port 5432.

2. Run RickAndMortyDataFetcher
   - Run the RickAndMortyDataFetcher project locally (as a console app)
   - It will:
     - Clear any existing data
     - Fetch characters from the Rick and Morty API
     - Seed the PostgreSQL database
3. Run RickAndMortyWebApp
    - Run the RickAndMortyWebApp project locally
    - It will open the browser automatically