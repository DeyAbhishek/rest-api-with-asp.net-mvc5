/*
	Stored procedure template
	note you need to ALWAYS test for existence of the sproc, then drop and recreate.

	ONE FILE PER STORED PROCEDURE.
*/
-- Test if the SPROC already exists. If so, DELETE it.
IF EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME  = 'MyStoredProcName'
	)
BEGIN
	DROP PROCEDURE [dbo].[MyStoredProcName]
END
GO

-- Now Create the Stored Procedure. A minimal comment block listing the intent is preferred.
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	MyStoredProcName
	Author:			Developer Name, eImagine Technology Group
	Created:		7/1/2004		

	Description:	This sproc performs the following .....

*/
CREATE PROCEDURE [dbo].[MyStoredProcName] ( 
	@Parameter1 int null
)
AS
BEGIN
	SELECT GETUTCDATE() [UTC]
END
GO




----------------------------------------------------------------------
--NOTE: This is for cleanup only. DO NOT INCLUDE IN REAL SCRIPTS
----------------------------------------------------------------------
DROP PROCEDURE [dbo].[MyStoredProcName]
GO
----------------------------------------------------------------------
