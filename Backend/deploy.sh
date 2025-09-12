#!/bin/bash

# Production deployment script for Task Management API

set -e

echo "🚀 Deploying Task Management API to Production..."

# Check if Docker is running
if ! docker info &> /dev/null; then
    echo "❌ Docker is not running. Please start Docker and try again."
    exit 1
fi

# Set production environment
export ASPNETCORE_ENVIRONMENT=Production

# Pull latest images
echo "📦 Pulling latest images..."
docker-compose pull

# Build production images
echo "🔨 Building production images..."
docker-compose -f docker-compose.yml -f docker-compose.prod.yml build --no-cache

# Stop existing services
echo "🛑 Stopping existing services..."
docker-compose -f docker-compose.yml -f docker-compose.prod.yml down

# Start services in production mode
echo "▶️  Starting services in production mode..."
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d

# Wait for services to be healthy
echo "⏳ Waiting for services to be ready..."
sleep 30

# Check service status
echo "📊 Service Status:"
docker-compose -f docker-compose.yml -f docker-compose.prod.yml ps

# Health check
echo "🏥 Performing health checks..."
if curl -f http://localhost:5000/health &> /dev/null; then
    echo "✅ API health check passed!"
else
    echo "❌ API health check failed!"
    docker-compose -f docker-compose.yml -f docker-compose.prod.yml logs api
    exit 1
fi

echo ""
echo "🎉 Production deployment successful!"
echo ""
echo "🌐 GraphQL Playground: http://localhost:5000/graphql"
echo "🏥 Health Check: http://localhost:5000/health"
echo ""
echo "📝 To view logs: docker-compose -f docker-compose.yml -f docker-compose.prod.yml logs -f"
echo "🛑 To stop: docker-compose -f docker-compose.yml -f docker-compose.prod.yml down"
