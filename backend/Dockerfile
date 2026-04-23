# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
# Note: Using wildcard for the root solution files if multiple exist
COPY ["SarvashresthaCMS.slnx", "./"]
COPY ["SarvashresthaCMS.WebApi/SarvashresthaCMS.WebApi.csproj", "SarvashresthaCMS.WebApi/"]
COPY ["SarvashresthaCMS.Application/SarvashresthaCMS.Application.csproj", "SarvashresthaCMS.Application/"]
COPY ["SarvashresthaCMS.Domain/SarvashresthaCMS.Domain.csproj", "SarvashresthaCMS.Domain/"]
COPY ["SarvashresthaCMS.Infrastructure/SarvashresthaCMS.Infrastructure.csproj", "SarvashresthaCMS.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "SarvashresthaCMS.WebApi/SarvashresthaCMS.WebApi.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR "/src/SarvashresthaCMS.WebApi"
RUN dotnet publish "SarvashresthaCMS.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "SarvashresthaCMS.WebApi.dll"]
