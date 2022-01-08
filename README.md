# NET FULL STACK DEVELOPER - MITOCODE COURSE - BACKEND

Repository with the source code developed in the couse for backend.

## COURSE LINK
https://www.mitocode.com/campus/#/principal/courses/modules/XBHN2$ZYTF3$JKUP7$

## URL FOR SWAGGER DOCUMENTATION
https://localhost:5001/swagger/index.html

## Command for start server from terminal
- `dotnet run --project .\ECommerceAPI\ECommerceAPI.csproj`

## COMMANDS USED IN PACKAGE MANAGER CONSOLE
- Create migration for Venta (Default project: DataAccess): `add-migragion TablaVenta`

## COMMANDS USED DOTNET CLI
- List migrations: `dotnet-ef migrations list --project .\ECommerceAPI\ECommerceAPI.csproj`
- Update database with migrations: `dotnet-ef database update --project .\ECommerceAPI\ECommerceAPI.csproj`

## COMMAND FOR START SQL SERVER CONTAINER WITH DOCKER
- `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123" -p 1433:1433 --name sqls_mitocode_netfs -d mcr.microsoft.com/mssql/server:2019-latest`
