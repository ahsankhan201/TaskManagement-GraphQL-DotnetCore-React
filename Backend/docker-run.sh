#!/bin/bash

# Script to run the Docker Compose setup

echo "🚀 Starting Task Management API with Docker Compose..."

# Check if Docker is running
if ! docker info &> /dev/null
then
    echo "❌ Docker is not running. Please start Docker and try again."
    exit 1
fi

# Build and start services
echo "📦 Building and starting services..."
docker-compose up --build -d

# Wait for services to be healthy
echo "⏳ Waiting for services to be ready..."
sleep 10

# Check service status
echo "📊 Service Status:"
docker-compose ps

echo ""
echo "✅ Services are starting up!"
echo ""
echo "🌐 GraphQL Playground: http://localhost:5000/graphql"
echo "🏥 Health Check: http://localhost:5000/health"
echo "📖 Swagger UI: http://localhost:5000/swagger"
echo ""
echo "📝 To view logs: docker-compose logs -f"
echo "🛑 To stop services: docker-compose down"
echo "🗑️  To remove all data: docker-compose down -v"


