CREATE TABLE [dbo].[EndOfRunTPOFeeder]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [WorkOrderID] INT NOT NULL, 
    [TPOExtruderID] INT NOT NULL, 
    [RawMaterialID] INT NOT NULL, 
    [QuantityUsed] FLOAT NOT NULL, 
    [Feeder] INT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_EndOfRunTPOFeeder_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_EndOfRunTPOFeeder_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]),
	CONSTRAINT [FK_EndOfRunTPOFeeder_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [WorkOrder]([ID]),
	CONSTRAINT [FK_EndOfRunTPOFeeder_TPOExtruder] FOREIGN KEY ([TPOExtruderID]) REFERENCES [TPOExtruder]([ID])
)
