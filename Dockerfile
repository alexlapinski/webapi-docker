FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build
WORKDIR /app

# Just restore the packages first (caching layer)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the files (not ignored) & Publish
COPY . ./
RUN dotnet publish -c Release -o out


# Prepare the final running image (with just binaries)
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine as runtime

WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "webapi-docker.dll"]
