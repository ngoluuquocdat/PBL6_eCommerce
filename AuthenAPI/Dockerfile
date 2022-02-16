#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AuthenAPI/AuthenAPI.csproj", "AuthenAPI/"]
COPY ["Database/Database.csproj", "Database/"]
RUN dotnet restore "AuthenAPI/AuthenAPI.csproj"
COPY . .
WORKDIR "/src/AuthenAPI"
RUN dotnet build "AuthenAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthenAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenAPI.dll"]