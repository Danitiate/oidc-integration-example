# Use an official .NET runtime as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the .NET project files to the container
COPY *.csproj ./

# Copy the rest of the application's source code to the container
COPY . ./

# Build the .NET app for release
RUN dotnet publish -c Release -o out

# Use a lightweight runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the built app from the build container
COPY --from=build /app/out .

# Expose the port your .NET app is listening on
EXPOSE 8081

# Start the .NET app
ENTRYPOINT ["dotnet", "OpenIdConnectIntegrationExample.dll"]
