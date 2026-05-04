FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy everything from build context (Backend)

COPY . .

# Restore using correct path

RUN dotnet restore Dijo/RestaurantAPI.csproj

# Publish using correct path

RUN dotnet publish Dijo/RestaurantAPI.csproj -c Release -o /app/out

# Runtime image

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "RestaurantAPI.dll"]