CREATE TABLE [dbo].[TPOLineScrapCode]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [Code] NVARCHAR(5) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
	[Group] INT NOT NULL,
    [View] BIT NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    CONSTRAINT [FK_TPOLineScrapCode_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID])
)
