# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Node.js layer for compiling Tailwind CSS
FROM node:latest as node
WORKDIR /web
COPY ["MVCManager/package.json", "MVCManager/package-lock.json*", "./"]
RUN npm install
COPY ["MVCManager/wwwroot/css/site.css", "MVCManager/tailwind.config.js", "./"]
RUN npm run css:build
COPY . .

# Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MVCManager/MVCManager.csproj", "MVCManager/"]
RUN dotnet restore "MVCManager/MVCManager.csproj"
COPY . .
WORKDIR "/src/MVCManager"
RUN dotnet build "MVCManager.csproj" -c Release -o /app/build

# Publish the .NET application
FROM build AS publish
RUN dotnet publish "MVCManager.csproj" -c Release -o /app/publish

# Final image for running the application
FROM base AS final
WORKDIR /app
COPY --from=node /web/styles.css ./wwwroot/css/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVCManager.dll"]
