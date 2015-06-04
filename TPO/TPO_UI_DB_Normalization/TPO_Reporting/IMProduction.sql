CREATE TABLE [dbo].[IMProduction]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ProductID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [LotID] INT NOT NULL, 
    [WorkOrderID] INT NOT NULL, 
    [WeightUoMID] INT NOT NULL, 
    [PartsCarton] INT NOT NULL, 
    [CartonWeight] FLOAT NOT NULL, 
    [BatchNumber] INT NOT NULL, 
    [Comments] NVARCHAR(MAX) NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_IMProduction_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_IMProduction_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]),
	CONSTRAINT [FK_IMProduction_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [WorkOrder]([ID]),
	CONSTRAINT [FK_IMProduction_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [UnitOfMeasure]([ID])
)
