# Docker Setup for React Spectrum Todo

This document explains how to build and run the React Spectrum Todo application using Docker.

## Prerequisites

- Docker Engine 20.10+
- Docker Compose 2.0+

## Environment Variables

Create a `.env` file in the project root with the following variables:

```bash
# GraphQL API Configuration
VITE_GRAPHQL_URL=https://localhost:7220/graphql

# Docker Configuration
COMPOSE_PROJECT_NAME=react-spectrum-todo
```

## Building and Running

### Production Build

1. **Build the Docker image:**
   ```bash
   docker build -t react-spectrum-todo .
   ```

2. **Run the container:**
   ```bash
   docker run -p 3000:80 -e VITE_GRAPHQL_URL=https://your-graphql-server.com/graphql react-spectrum-todo
   ```

3. **Or use Docker Compose:**
   ```bash
   docker-compose up -d
   ```

### Development Build

1. **Run development container:**
   ```bash
   docker-compose --profile dev up react-spectrum-todo-dev
   ```

2. **Access the application:**
   - Production: http://localhost:3000
   - Development: http://localhost:5173

## Docker Commands

### Build Commands
```bash
# Build production image
docker build -t react-spectrum-todo .

# Build development image
docker build -f Dockerfile.dev -t react-spectrum-todo:dev .

# Build with Docker Compose
docker-compose build
```

### Run Commands
```bash
# Run production container
docker run -p 3000:80 react-spectrum-todo

# Run development container
docker run -p 5173:5173 -v $(pwd):/app react-spectrum-todo:dev

# Run with Docker Compose
docker-compose up -d
```

### Management Commands
```bash
# Stop containers
docker-compose down

# View logs
docker-compose logs -f

# Restart services
docker-compose restart

# Remove containers and volumes
docker-compose down -v
```

## Image Details

### Production Image
- **Base Image:** nginx:alpine
- **Size:** ~25MB
- **Port:** 80 (mapped to 3000)
- **Health Check:** Built-in
- **Features:**
  - Multi-stage build for optimization
  - Gzip compression
  - Security headers
  - Static asset caching
  - Client-side routing support

### Development Image
- **Base Image:** node:18-alpine
- **Size:** ~200MB
- **Port:** 5173
- **Features:**
  - Hot reloading
  - Volume mounting for live code changes
  - Development dependencies included

## Troubleshooting

### Common Issues

1. **Port already in use:**
   ```bash
   # Change port in docker-compose.yml
   ports:
     - "3001:80"  # Use different host port
   ```

2. **GraphQL connection issues:**
   ```bash
   # Update VITE_GRAPHQL_URL in .env file
   VITE_GRAPHQL_URL=https://your-actual-graphql-server.com/graphql
   ```

3. **Build failures:**
   ```bash
   # Clean Docker cache
   docker system prune -a
   
   # Rebuild without cache
   docker build --no-cache -t react-spectrum-todo .
   ```

### Health Checks

The production container includes health checks:
```bash
# Check container health
docker ps

# View health check logs
docker inspect react-spectrum-todo | grep Health -A 10
```

## Security Notes

- The production image includes security headers
- Uses non-root user in nginx
- Implements CSP (Content Security Policy)
- Static assets are cached appropriately

## Performance Optimization

- Multi-stage build reduces final image size
- Nginx serves static files efficiently
- Gzip compression enabled
- Long-term caching for static assets
