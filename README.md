# The-CRUD-App


## Local Development

- Run PostgreSQL (docker/dockercompose or direct install)
  - Docker Compose
    - configure volume in docker-compose.yaml (optional)
    - run cmd 
        > cd ./Local \
        docker-compose up -d the-crud-app_postgres
  - Local
    > https://www.postgresql.org/download/

- Run DB Migration
  - install dotnet entity framework tool
    > dotnet tool install --global dotnet-ef
  - run migration
    > ef database update --project TheCrudApp\TheCrudApp.csproj --startup-project TheCrudApp\TheCrudApp.csproj --context TheCrudApp.Database.DatabaseContext


## Run API in Docker Container
  - run cmd (build.cmd / build.sh)
    > docker build -t the-crud-app . 
  - start container
    > docker-compose up -d the-crud-app_api