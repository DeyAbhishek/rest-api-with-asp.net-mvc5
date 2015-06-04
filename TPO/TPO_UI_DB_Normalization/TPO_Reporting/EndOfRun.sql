CREATE TABLE [dbo].[EndOfRun]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LineID] INT NOT NULL, 
    [PlantID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [WorkOrderID] INT NOT NULL, 
    [AreaUsed] FLOAT NOT NULL, 
	[DrainedResin] FLOAT NOT NULL,
    [WeightUsed] FLOAT NOT NULL, 
	[DateEntered] DATETIME NOT NULL, 
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
	[ModifiedBy] NVARCHAR(100) NOT NULL,
    CONSTRAINT [FK_EndOfRun_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]), 
    CONSTRAINT [FK_EndOfRun_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]), 
    CONSTRAINT [FK_EndOfRun_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [WorkOrder]([ID])
)
