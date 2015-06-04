CREATE TABLE [dbo].[ScrimActionType]
(
	[ID] INT NOT NULL PRIMARY KEY, 
	[PlantID] INT NOT NULL,
    [Code] NVARCHAR(5) NOT NULL, 
    [Description] NVARCHAR(100) NOT NULL, 
    [SortOrder] INT NOT NULL DEFAULT 0, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_ScrimActionType_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID])
)

GO

CREATE UNIQUE INDEX [IX_ScrimActionType_Code] ON [dbo].[ScrimActionType] ([Code])
