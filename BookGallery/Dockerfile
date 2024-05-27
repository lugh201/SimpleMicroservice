# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Use the official ASP.NET Core SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
WORKDIR /src
COPY ["BookGallery/BookGallery.csproj", "BookGallery/"]
RUN dotnet restore "BookGallery/BookGallery.csproj"
COPY . .
WORKDIR "/src/BookGallery"
RUN dotnet build "BookGallery.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "BookGallery.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# Use the runtime image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookGallery.dll"]
