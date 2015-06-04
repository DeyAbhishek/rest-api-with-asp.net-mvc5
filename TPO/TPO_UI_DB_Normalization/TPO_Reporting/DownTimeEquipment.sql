CREATE TABLE [dbo].[DownTimeEquipment]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DownTimeTypeID] INT NOT NULL, 
    [PlantID] INT NOT NULL, 
	[LineID] INT NULL,
    [Description] NVARCHAR(100) NOT NULL, 
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL,
	[ModifiedBy] NVARCHAR(100) NOT NULL 
    CONSTRAINT [FK_DownTimeEquipment_DownTimeType] FOREIGN KEY ([DownTimeTypeID]) REFERENCES [DownTimeType]([ID]), 
    CONSTRAINT [FK_DownTimeEquipment_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID])
)

GO
