# 🐳 Docker Setup for GraphQL Task Management API

This project includes complete Docker containerization with Docker Compose orchestration for both the backend API and SQL Server database.

## 📋 Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running
- [Docker Compose](https://docs.docker.com/compose/) (included with Docker Desktop)

## 🚀 Quick Start

### Option 1: Using the convenient script
```bash
# Make script executable (Linux/Mac)
chmod +x docker-run.sh

# Run the script
./docker-run.sh
```

### Option 2: Manual Docker Compose commands
```bash
# Build and start all services
docker-compose up --build -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down

# Stop and remove all data
docker-compose down -v
```

## 🏗️ Architecture

### Services

1. **API Service** (`api`)
   - **Image**: Custom built from Dockerfile
   - **Port**: 5000 (mapped to internal 8080)
   - **Environment**: Development
   - **Health Check**: `/health` endpoint

2. **Database Service** (`sqlserver`)
   - **Image**: `mcr.microsoft.com/mssql/server:2022-latest`
   - **Port**: 1433
   - **Credentials**: `sa` / `StrongPassword123!`
   - **Persistent Storage**: Docker volume `sqlserver_data`

### Network
- **Custom Bridge Network**: `taskmanagement-network`
- Services communicate via service names (e.g., `api` can reach `sqlserver`)

## 🔗 Access Points

Once running, access the application at:

- **GraphQL Playground**: http://localhost:5000/graphql
- **Health Check**: http://localhost:5000/health
- **Swagger Documentation**: http://localhost:5000/swagger

## 📁 Docker Files

### `Dockerfile`
Multi-stage build for the .NET API:
- **Build Stage**: Uses .NET 8 SDK to build the application
- **Runtime Stage**: Uses .NET 8 runtime for production deployment

### `docker-compose.yml`
Main orchestration file defining:
- SQL Server with persistent storage
- API service with health checks
- Network configuration
- Volume management

### `docker-compose.override.yml`
Development-specific overrides:
- Additional port mappings
- Development environment variables
- Volume mounts for development

## ⚙️ Configuration

### Environment Variables

Key environment variables (defined in `docker.env`):

```bash
# Database
SA_PASSWORD=StrongPassword123!
DB_NAME=TaskManagementDb

# API
API_PORT=5000
ASPNETCORE_ENVIRONMENT=Development
```

### Connection String

The API uses this connection string for Docker:
```
Server=sqlserver,1433;Database=TaskManagementDb;User ID=sa;Password=StrongPassword123!;MultipleActiveResultSets=true;Trust Server Certificate=True;Encrypt=False;
```

## 🔧 Common Docker Commands

### Development
```bash
# Start in development mode
docker-compose up

# Rebuild after code changes
docker-compose up --build

# View live logs
docker-compose logs -f api

# Execute commands in running container
docker-compose exec api bash
```

### Maintenance
```bash
# Check service status
docker-compose ps

# Stop all services
docker-compose down

# Remove everything including volumes
docker-compose down -v --remove-orphans

# Pull latest images
docker-compose pull
```

### Database Management
```bash
# Connect to SQL Server
docker-compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P StrongPassword123!

# Backup database
docker-compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P StrongPassword123! -Q "BACKUP DATABASE TaskManagementDb TO DISK = '/var/opt/mssql/backup/TaskManagementDb.bak'"

# Restore database
docker-compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P StrongPassword123! -Q "RESTORE DATABASE TaskManagementDb FROM DISK = '/var/opt/mssql/backup/TaskManagementDb.bak'"
```

## 🧪 Testing the Setup

### 1. Health Check
```bash
curl http://localhost:5000/health
```

### 2. GraphQL Query
```graphql
query {
  getAllTasksAsync {
    id
    title
    description
    status
    createdAt
  }
}
```

### 3. Create a Task
```graphql
mutation {
  createTaskAsync(input: {
    title: "Docker Test Task"
    description: "Testing the dockerized API"
    status: PENDING
  }) {
    id
    title
    status
    createdAt
  }
}
```

## 🐛 Troubleshooting

### Common Issues

1. **Port Already in Use**
   ```bash
   # Check what's using port 5000
   netstat -tulpn | grep 5000
   
   # Change port in docker-compose.yml
   ports:
     - "5001:8080"  # Use different port
   ```

2. **SQL Server Connection Issues**
   ```bash
   # Check SQL Server logs
   docker-compose logs sqlserver
   
   # Test connection
   docker-compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P StrongPassword123! -Q "SELECT 1"
   ```

3. **API Not Starting**
   ```bash
   # Check API logs
   docker-compose logs api
   
   # Rebuild from scratch
   docker-compose down -v
   docker-compose up --build
   ```

### Performance Tips

1. **Use .dockerignore**: Already configured to exclude unnecessary files
2. **Multi-stage builds**: Dockerfile uses optimized multi-stage build
3. **Health checks**: Services include health checks for reliable startup
4. **Persistent volumes**: Database data persists between container restarts

## 🔄 Production Deployment

For production deployment:

1. Update `docker-compose.yml`:
   ```yaml
   environment:
     - ASPNETCORE_ENVIRONMENT=Production
   ```

2. Use environment-specific configuration:
   ```bash
   docker-compose -f docker-compose.yml -f docker-compose.prod.yml up
   ```

3. Consider using orchestration platforms like Kubernetes or Docker Swarm for scaling.

## 📝 Notes

- **Data Persistence**: Database data is stored in Docker volume `sqlserver_data`
- **Development**: Code changes require rebuilding the API container
- **Security**: Default passwords are for development only - change for production
- **Networking**: Services can communicate using service names as hostnames
