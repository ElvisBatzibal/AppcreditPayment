DECLARE	@return_value int

EXEC	@return_value = [dbo].[Sp_Account]
		@NumConsulta = 1,
		@AccountName = N'CREDITO 2',
		@Currency = N'QTZ',
		@CardCode = N'10',
		@InitialBalance = N'5000'

SELECT	'Return Value' = @return_value

SELECT TOP 1 * FROM Account ORDER BY 1 DESC;
SELECT TOP 1 * FROM Customer ORDER BY 1 DESC;
GO
