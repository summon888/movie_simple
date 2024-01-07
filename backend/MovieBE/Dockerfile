# .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Sets the working directory
WORKDIR /app

# Copy Projects
#COPY *.sln .
COPY Application/Application.csproj ./src/Application/
COPY Domain/Domain.csproj ./src/Domain/
COPY Domain.Core/Domain.Core.csproj ./src/Domain.Core/
COPY Identity/Identity.csproj ./src/Identity/
COPY Infrastructure/Infrastructure.csproj ./src/Infrastructure/
COPY IoC/IoC.csproj ./src/IoC/
COPY WebAPI/WebAPI.csproj ./src/WebAPI/

# .NET Core Restore
RUN dotnet restore ./src/WebAPI/WebAPI.csproj

# Copy All Files
COPY src ./src

# .NET Core Build and Publish
RUN dotnet publish ./src/WebAPI/WebAPI.csproj -c Release -o /publish

# ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish ./

# Expose ports
EXPOSE 80
EXPOSE 443

# Setup your variables before running.
ARG MyEnv
ENV ASPNETCORE_ENVIRONMENT $MyEnv

ENTRYPOINT ["dotnet", "WebAPI.dll"]