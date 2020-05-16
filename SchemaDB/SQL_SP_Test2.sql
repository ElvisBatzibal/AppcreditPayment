DECLARE	@return_value int

EXEC	@return_value = [dbo].[Sp_CreditAccount]
		@NumConsulta = 1,
		@AccountNum = N'8',
		@DocTotal = N'1000',
		@CardCode = NULL

SELECT	'Return Value' = @return_value
SELECT TOP 1 * FROM CreditAccount ORDER BY 1 DESC;;
SELECT TOP 1 * FROM Account ORDER BY 1 DESC;;
SELECT TOP 1 * FROM Customer ORDER BY 1 DESC;;

GO
