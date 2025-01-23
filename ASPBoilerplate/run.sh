#!/bin/bash

# Check if Redis server is running
if ! pgrep -x "redis-server" > /dev/null
then
    echo "Redis server is not running. Starting Redis..."
    brew services start redis
else
    echo "Redis server is already running."
fi

# Run the .NET application
echo "Running the .NET application..."
dotnet watch