CREATE TABLE [dbo].[DownTimeType]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] NVARCHAR(5) NOT NULL, 
    [Description] NVARCHAR(20) NOT NULL, 
    [SortOrder] INT NOT NULL DEFAULT 0, 
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL
)
