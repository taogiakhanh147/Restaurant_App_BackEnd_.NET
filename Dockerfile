# Build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution and project
COPY RestaurantAPI.sln .
COPY RestaurantAPI/ ./RestaurantAPI/

# Restore & build
RUN dotnet restore
RUN dotnet publish RestaurantAPI/RestaurantAPI.csproj -c Release -o /app/publish

# Run image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 80
ENTRYPOINT ["dotnet", "RestaurantAPI.dll"]
