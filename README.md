# ChatApp - Real-Time Chat with Sentiment Analysis

The app has two parts:
- `ChatApp.Backend` - ASP.NET Core API + SignalR Hub + message persistence.
- `ChatApp.Frontend` - React + Vite chat client.

## Implemented Features

- Real-time chat with SignalR:
  - messages are sent through `ChatHub`;
  - all connected clients receive updates instantly.
- Message history:
  - API `GET /api/messages?count=10` returns recent messages;
  - messages are stored in the database.
- Sentiment analysis:
  - in Azure mode, the app uses **Azure Text Analytics**;
  - locally, it uses `StubSentimentHandler` (simple word-based heuristic), so the project works without cloud keys.
- Sentiment UI:
  - each message is marked as Positive / Neutral / Negative;
  - color highlighting and emoji are used for quick visual feedback.
- Data mode:
  - local mode: SQLite (`chat_local.db`);
  - Azure mode: SQL Server (Azure SQL) when a connection string is provided.

## Tech Stack

- **Backend**: .NET 8, ASP.NET Core, SignalR, EF Core, FluentValidation, AutoMapper.
- **Frontend**: React, Vite, TypeScript, Tailwind, `@microsoft/signalr`, shadcn.
- **Cloud (task scope)**: Azure Web App, Azure SignalR Service, Azure SQL, Azure Language Service.

## Repository Structure

- `ChatApp.Backend` - server side.
- `ChatApp.Frontend` - web client.

## Run Locally

### 1) Backend

```bash
cd ChatApp.Backend
dotnet restore
dotnet run
```

By default, backend runs on `http://localhost:5124`.

### 2) Frontend

```bash
cd ChatApp.Frontend
npm install
npm run dev
```

By default, frontend runs on `http://localhost:5173` and connects to backend via `VITE_BACKEND_URL`.

## Configuration

### Frontend env

Create `.env` in `ChatApp.Frontend` (you can copy from `.env.example`):

```env
VITE_BACKEND_URL=http://localhost:5124
```

### Backend appsettings

Main settings in `ChatApp.Backend/appsettings.json`:

- `ConnectionStrings:DefaultConnection` - local SQLite.
- `ConnectionStrings:AzureSQL` - Azure SQL connection string.
- `Azure:SignalR:ConnectionString` - Azure SignalR connection string.
- `AzureTextAnalytics:Endpoint` and `AzureTextAnalytics:Key` - for Azure sentiment analysis.
- `Frontend:Url` - CORS origin.

Runtime mode selection:
- if **both** `Azure:SignalR:ConnectionString` and `ConnectionStrings:AzureSQL` are set, backend runs in Azure mode (Azure SignalR + SQL Server + Azure sentiment API);
- otherwise, local mode is used (SQLite + stub sentiment).

## Task Requirements Coverage

1. **Azure Web App for backend and UI**  
   The project is split into backend/frontend and is ready for separate Web App deployments.

2. **Azure SignalR Service**  
   Supported via `Microsoft.Azure.SignalR` and `Azure:SignalR:ConnectionString`.

3. **Real-time chat**  
   Implemented via `ChatHub` and client connection to `/chatHub`.

4. **Sentiment analysis (optional)**  
   Integrated with Azure Language Service, with a local stub fallback.

5. **Data Storage**  
   Messages and sentiment results are stored in the database (`Messages`).

6. **UI enhancements**  
   Messages are visually highlighted by sentiment (color/emoji/label).

## Azure Deployment (Brief)

1. Create resources:
   - Azure Web App for backend;
   - Azure Web App (or Static Web App) for frontend;
   - Azure SignalR Service;
   - Azure SQL Database;
   - Azure Cognitive Services (Text Analytics), if you use sentiment API.
2. Configure backend environment variables (connection strings and Azure keys).
3. Set `VITE_BACKEND_URL` in frontend to the backend app URL.
4. Verify CORS (`Frontend:Url`) and websocket/SignalR connectivity.

## Notes

- In the current version, messages are sent without authentication and without a dedicated username flow.
- Local database `chat_local.db` may contain test data.
