CREATE TABLE [dbo].[ProductionShiftUse]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [UseShift] BIT NOT NULL, 
    [Day1Minutes] INT NOT NULL DEFAULT 0, 
    [Day2Minutes] INT NOT NULL DEFAULT 0, 
    [Day3Minutes] INT NOT NULL DEFAULT 0, 
    [Day4Minutes] INT NOT NULL DEFAULT 0, 
    [Day5Minutes] INT NOT NULL DEFAULT 0, 
    [Day6Minutes] INT NOT NULL DEFAULT 0, 
    [Day7Minutes] INT NOT NULL DEFAULT 0, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_ProductionShiftUse_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_ProductionShiftUse_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID])
)
