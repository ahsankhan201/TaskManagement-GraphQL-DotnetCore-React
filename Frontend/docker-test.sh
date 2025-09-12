#!/bin/bash

echo "Testing Docker setup for React Spectrum Todo..."

echo ""
echo "1. Checking if Docker is installed..."
if ! command -v docker &> /dev/null; then
    echo "ERROR: Docker is not installed or not in PATH"
    echo "Please install Docker from: https://docs.docker.com/get-docker/"
    exit 1
fi

echo "Docker is installed:"
docker --version

echo ""
echo "2. Checking Docker Compose..."
if ! command -v docker-compose &> /dev/null && ! docker compose version &> /dev/null; then
    echo "ERROR: Docker Compose not available"
    exit 1
fi

if command -v docker-compose &> /dev/null; then
    echo "Docker Compose is available:"
    docker-compose --version
else
    echo "Docker Compose is available (via 'docker compose'):"
    docker compose version
fi

echo ""
echo "3. Validating Dockerfile syntax..."
if [ ! -f "Dockerfile" ]; then
    echo "ERROR: Dockerfile not found"
    exit 1
fi

echo "Dockerfile found and ready to build."

echo ""
echo "4. Ready to build! Run one of these commands:"
echo ""
echo "For production build:"
echo "  docker build -t react-spectrum-todo ."
echo ""
echo "For development build:"
echo "  docker build -f Dockerfile.dev -t react-spectrum-todo:dev ."
echo ""
echo "To run with Docker Compose:"
echo "  docker-compose up -d"
echo ""
echo "To run development with Docker Compose:"
echo "  docker-compose --profile dev up react-spectrum-todo-dev"

echo ""
echo "Setup complete!"
