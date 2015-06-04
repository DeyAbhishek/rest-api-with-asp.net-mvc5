CREATE TABLE [dbo].[ProductionBudgetType]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] NVARCHAR(10) NOT NULL, 
    [Description] NVARCHAR(50) NULL,
	[SortOrder] INT NOT NULL DEFAULT 0,
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL
)
