# Используем официальный образ .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Копируем проект бэкенда
COPY Notes.Backend/Notes.WebApi/Notes.WebApi.csproj Notes.Backend/
RUN dotnet restore Notes.Backend/Notes.WebApi/Notes.WebApi.csproj

# Копируем остальной код
COPY . .
RUN dotnet publish Notes.Backend/Notes.WebApi/Notes.WebApi.csproj -c Release -o out

# Используем runtime-образ
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Notes.WebApi.dll"]