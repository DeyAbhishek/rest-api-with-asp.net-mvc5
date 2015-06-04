CREATE TABLE [dbo].[EndOfRunRework]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [SlipSheetWeight] FLOAT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_EndOfRunRework_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_EndOfRunRework_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID])
)
