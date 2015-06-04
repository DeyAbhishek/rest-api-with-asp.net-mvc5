CREATE TABLE [dbo].[RawMaterialReceivedSizeLimit]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [Code] NVARCHAR(100) NOT NULL, 
    [View] BIT NOT NULL, 
    [LowLimit] FLOAT NOT NULL, 
    [HighLimit] FLOAT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_RawMaterialReceivedSizeLimit_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID])
)
