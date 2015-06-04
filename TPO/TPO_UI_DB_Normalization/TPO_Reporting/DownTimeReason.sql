CREATE TABLE [dbo].[DownTimeReason]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NULL, 
	[DownTimeTypeID] INT NOT NULL,
    [Description] NVARCHAR(100) NOT NULL, 
	[Scheduled] BIT NOT NULL
    CONSTRAINT [FK_DownTimeReason_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	[SortOrder] INT NOT NULL DEFAULT 0, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_DownTimeReason_DownTimeType] FOREIGN KEY ([DownTimeTypeID]) REFERENCES [DownTimeType]([ID])
)
