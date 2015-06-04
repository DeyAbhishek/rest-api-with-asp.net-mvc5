CREATE TABLE [dbo].[RawMaterialActionType]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] NVARCHAR(5) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    [SortOrder] INT NOT NULL DEFAULT 0,
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [LastModifiedBy] NVARCHAR(100) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_RawMaterialActionType_Code] ON [dbo].[RawMaterialActionType] ([Code])
