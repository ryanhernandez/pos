# POS Backend (Server)

Lightweight .NET 8 Web API backend scaffolded with DDD modular structure, HotChocolate GraphQL, and EF Core (SQLite).

Structure highlights:
- `Modules/Inventory` — fully implemented example module (Domain, Infrastructure, GraphQL)
- `Shared/Kernel` — EntityBase, ValueObject, DomainEvent
- `Shared/Infrastructure` — AppDbContext and EF helpers

Run locally (development):

1. Ensure .NET 8 SDK is installed.
2. From `/Server`:

```powershell
dotnet restore
dotnet ef database update
dotnet run
```

Open GraphQL playground at: http://localhost:5000/graphql (or the port printed by dotnet run). Health check: GET /health

Docker (build & run):

```powershell
docker compose up --build
```

This will build and run the API. The SQLite file is persisted to a Docker volume named `pos_data`.

GraphQL examples
----------------

Inventory - query items:

```graphql
query {
	getItems {
		id
		name
		description
		price
		quantity
	}
}
```

Inventory - add item:

```graphql
mutation {
	addItem(input: { name: "Widget", price: 9.99, quantity: 10, description: "Small widget" }) {
		id
		name
	}
}
```

Users - create and query:

```graphql
mutation {
	createUser(input: { username: "alice", email: "alice@example.com", displayName: "Alice" }) {
		id
		username
		email
	}
}

query {
	getUsers {
		id
		username
		email
		displayName
	}
}
```

Notes & how-to:
- Add new modules by following `Modules/Inventory` pattern: create Domain, Infrastructure, GraphQL folders and an `XModule.cs` to register DI types.
- Use EF Core migrations: `dotnet ef migrations add InitialCreate -p Server -s Server` and `dotnet ef database update -p Server -s Server`.

Run tests from repo root
------------------------

If you want a single command to run all unit tests from the repository root (so you don't need to remember the test project path), use the provided convenience scripts:

PowerShell (Windows):

```powershell
.\test.ps1
```

Windows (cmd):

```cmd
.\test.cmd
```

Both wrappers run `dotnet test .\pos.sln` and forward additional arguments, e.g. `./test.ps1 --filter "FullyQualifiedName~InventoryService"`.
