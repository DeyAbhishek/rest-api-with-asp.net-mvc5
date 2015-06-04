CREATE TABLE [dbo].[ProductionLineSchedule]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [ProductionDate] DATE NOT NULL, 
    [MinutesScheduled] INT NOT NULL DEFAULT 0, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_ProductionLineSchedule_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_ProductionLineSchedule_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID])
)
