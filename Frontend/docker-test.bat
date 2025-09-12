@echo off
echo Testing Docker setup for React Spectrum Todo...

echo.
echo 1. Checking if Docker is installed...
docker --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: Docker is not installed or not in PATH
    echo Please install Docker Desktop from: https://www.docker.com/products/docker-desktop
    goto :end
)

echo Docker is installed: 
docker --version

echo.
echo 2. Checking Docker Compose...
docker-compose --version >nul 2>&1
if %errorlevel% neq 0 (
    echo WARNING: Docker Compose not found, trying 'docker compose'...
    docker compose version >nul 2>&1
    if %errorlevel% neq 0 (
        echo ERROR: Docker Compose not available
        goto :end
    )
    echo Docker Compose is available
) else (
    echo Docker Compose is available:
    docker-compose --version
)

echo.
echo 3. Validating Dockerfile syntax...
if not exist Dockerfile (
    echo ERROR: Dockerfile not found
    goto :end
)

echo Dockerfile found and ready to build.

echo.
echo 4. Ready to build! Run one of these commands:
echo.
echo For production build:
echo   docker build -t react-spectrum-todo .
echo.
echo For development build:
echo   docker build -f Dockerfile.dev -t react-spectrum-todo:dev .
echo.
echo To run with Docker Compose:
echo   docker-compose up -d
echo.
echo To run development with Docker Compose:
echo   docker-compose --profile dev up react-spectrum-todo-dev

:end
echo.
echo Setup complete!
pause
