CREATE TABLE [dbo].[ProductionShiftType]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] NVARCHAR(5) NOT NULL, 
    [Description] NVARCHAR(2000) NOT NULL,
	[SortOrder] INT NOT NULL DEFAULT 0,
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
	[LastModified] DATETIME NOT NULL,
	[ModifiedBy] NVARCHAR(100) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_ProductionShiftType_Code] ON [dbo].[ProductionShiftType] ([Code])
