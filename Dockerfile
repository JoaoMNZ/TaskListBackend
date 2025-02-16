    FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

    WORKDIR /src
    COPY ["TaskListAPI.csproj", "./"]
    RUN dotnet restore "./TaskListAPI.csproj"

    COPY . .
    WORKDIR "/src/"
    RUN dotnet build 'TaskListAPI.csproj' -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish 'TaskListAPI.csproj' -c Release -o /app/publish

    FROM mcr.microsoft.com/dotnet/aspnet:9.0
    ENV ASPNETCORE_URLS=http://+:5242
    EXPOSE 5242
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "TaskListAPI.dll"]