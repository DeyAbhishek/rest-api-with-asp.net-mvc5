CREATE TABLE [dbo].[TPOFormulation]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(100) NULL, 
    [ExtruderCount] INT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_TPOFormulation_Code] ON [dbo].[TPOFormulation] ([Code])
