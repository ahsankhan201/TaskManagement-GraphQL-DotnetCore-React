#!/bin/bash
# Script to wait for SQL Server to be ready

host="$1"
port="$2"
password="$3"

echo "Waiting for SQL Server at $host:$port..."

until /opt/mssql-tools/bin/sqlcmd -S "$host,$port" -U sa -P "$password" -Q "SELECT 1" &> /dev/null
do
  echo "SQL Server is unavailable - sleeping"
  sleep 1
done

echo "SQL Server is up and running!"

