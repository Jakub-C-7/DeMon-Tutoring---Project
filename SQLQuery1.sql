﻿USE [DeMon_Tutoring_Db]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[sproc_TblStaffing_SelectAll]

SELECT	@return_value as 'Return Value'

GO
