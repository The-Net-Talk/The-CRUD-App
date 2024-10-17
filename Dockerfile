FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY  ./TheCrudApp ./src
WORKDIR /src
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 5000
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "TheCrudApp.dll"]