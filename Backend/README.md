# Task Management GraphQL API

A .NET 8 GraphQL API for task management with Entity Framework Core and SQL Server integration. Features complete CRUD operations through GraphQL queries and mutations with Docker containerization support.

## Features

- **GraphQL API**: Complete task management through HotChocolate GraphQL server
- **Entity Framework Core**: Database operations with SQL Server
- **Docker Support**: Multi-container setup with docker-compose
- **Service Layer**: Clean architecture with dedicated business logic layer
- **Unit Testing**: Comprehensive test coverage with XUnit and in-memory database
- **Health Checks**: Database health monitoring endpoint
- **CORS Support**: Cross-origin resource sharing configured

## Task Operations

### GraphQL Queries
-  Retrieve all tasks ordered by creation date
-  Get specific task by ID
-  Filter tasks by status

### GraphQL Mutations
-  Create new task
-  Update existing task
-  Change task status only
-  Remove task

### Task Status Enum
- `Pending`
- `InProgress` 
- `Completed`
- `Cancelled`

## Technology Stack

- **.NET 8**: Web API framework
- **HotChocolate**: GraphQL server for .NET
- **Entity Framework Core 8.0.8**: ORM for database operations
- **SQL Server**: Database management system
- **Docker**: Containerization platform
- **XUnit**: Unit testing framework

## Project Structure

```
GraphQL/
├── Data/
│   └── TaskDbContext.cs              # Entity Framework DbContext
├── Models/
│   ├── Task.cs                       # Task entity model
│   └── TaskStatus.cs                 # Task status enumeration
├── Services/
│   ├── ITaskService.cs               # Service interface
│   └── TaskService.cs                # Business logic implementation
├── GraphQL/
│   ├── Queries/
│   │   └── TaskQueries.cs            # GraphQL query resolvers
│   ├── Mutations/
│   │   └── TaskMutations.cs          # GraphQL mutation resolvers
│   ├── Types/
│   │   ├── CreateTaskInput.cs        # Input type for task creation
│   │   └── UpdateTaskStatusInput.cs  # Input type for status updates
│   └── Examples/
│       └── sample-queries.graphql    # Sample GraphQL operations
├── GraphQL.Tests/
│   └── Services/
│       └── TaskServiceTests.cs       # Unit tests for TaskService
├── Migrations/                       # Entity Framework migrations
├── scripts/
│   └── wait-for-db.sh               # Database startup script
├── docker-compose.yml               # Docker services configuration
├── docker-compose.override.yml      # Development overrides
├── docker-compose.prod.yml          # Production configuration
├── Dockerfile                       # API container definition
└── Program.cs                       # Application startup
```

## Getting Started

### Option 1: Docker Compose (Recommended)

1. **Prerequisites**
   - Docker Desktop installed and running

2. **Start the application**
   ```bash
   docker-compose up --build
   ```

3. **Access the application**
   - GraphQL Playground: http://localhost:5000/graphql
   - Health Check: http://localhost:5000/health

4. **Stop the application**
   ```bash
   docker-compose down
   ```

### Option 2: Local Development

1. **Prerequisites**
   - .NET 8 SDK
   - SQL Server or SQL Server LocalDB

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update database**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

## Docker Configuration

### Services
- **sqlserver**: SQL Server 2022 Express container
- **api**: .NET 8 GraphQL API container

### Environment Variables
- `ASPNETCORE_ENVIRONMENT`: Application environment (Development/Production)
- `ConnectionStrings__DefaultConnection`: Database connection string
- `SA_PASSWORD`: SQL Server SA password (StrongPassword123!)

### Ports
- **API**: http://localhost:5000
- **SQL Server**: localhost:1433

## Database Configuration

The application uses SQL Server with Entity Framework Core migrations.

### Connection Strings
- **Local Development**: Uses LocalDB
- **Docker**: Uses containerized SQL Server
- **Production**: Configurable via appsettings.json

### Database Initialization
- Database is automatically created on first run
- Initial schema applied through EF migrations

## Testing

### Running Unit Tests
```bash
dotnet test GraphQL.Tests
```

### Test Coverage
- TaskService business logic
- CRUD operations
- Input validation
- Error handling scenarios

### Test Database
Tests use Entity Framework In-Memory database provider for isolation.

## API Usage

### Sample Queries

Access the GraphQL playground at http://localhost:5000/graphql and try these operations:


## Health Monitoring

The application includes health check endpoints:
- `/health`: Overall application health
- Database connectivity verification

## Configuration Files

- `appsettings.json`: Base configuration
- `appsettings.Development.json`: Development overrides  
- `appsettings.Docker.json`: Docker-specific settings

## Troubleshooting

### Logs
Application logs are available in console output and can be configured in appsettings.json.

---

Built with .NET 8 and HotChocolate GraphQL