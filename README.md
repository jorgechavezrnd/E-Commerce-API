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

## QUERIES FOR CREATE PROCEDURES IN SQL SERVER
``` sql
CREATE PROCEDURE uspSelectDetails
(
	@SaleId NVARCHAR(36)
)
AS
BEGIN

	SELECT
		SD.Id,
		SD.ItemNumber,
		P.Name As ProductName,
		SD.Quantity,
		SD.UnitPrice,
		SD.Total
	FROM SaleDetail SD
	INNER JOIN Products P ON P.Id = SD.ProductId
	WHERE SD.Status = 1
	AND SD.SaleId = @SaleId

END
GO

CREATE PROCEDURE uspReportByMonth
(
	@Month int,
	@Year int
)
AS
BEGIN

	SELECT
		DAY(S.SaleDate) As Day,
		SUM(S.TotalSale) TotalSales
	FROM Sale S
	WHERE S.Status = 1
	AND MONTH(S.SaleDate) = @Month
	AND YEAR(S.SaleDate) = @Year
	GROUP BY DAY(S.SaleDate)
	ORDER BY 1

END
GO
```

## EXAMPLES OF QUERIES FOR EXECUTE PROCEDURES
``` sql
select * from Sale

exec uspSelectDetails 'ca763cd0-ff2c-42ad-a060-2e4cdf2f926e'

EXEC uspReportByMonth 10, 2021
```
