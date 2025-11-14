# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["DbSetup/DbSetup.csproj", "DbSetup/"]
RUN dotnet restore "DbSetup/DbSetup.csproj"
COPY . .
WORKDIR /src/DbSetup
RUN dotnet publish "DbSetup.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "DbSetup.dll"]