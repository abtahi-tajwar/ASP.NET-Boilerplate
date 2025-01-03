#!/bin/bash

# Check if app.db exists and delete it
if [ -f "./app.db" ]; then
    echo "Deleting app.db..."
    rm ./app.db
    echo "app.db deleted."
else
    echo "app.db not found. Skipping deletion."
fi

# Run the commands
echo "Removing last migration..."
dotnet ef migrations remove

echo "Adding new migration..."
dotnet ef migrations add InitialMigration -o Migrations

echo "Updating the database..."
dotnet ef database update

echo "Process completed!"
