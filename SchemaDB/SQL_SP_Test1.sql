DECLARE	@return_value int

EXEC	@return_value = [dbo].[Sp_Customer]
		@NumConsulta = 1,
		@CardName = N'SANTOS MARIO',
		@Address = N'GUATEMALA',
		@Tel = N'12345678'
SELECT	'Return Value' = @return_value
SELECT TOP 1  * FROM Customer 
ORDER BY 1 DESC
GO

