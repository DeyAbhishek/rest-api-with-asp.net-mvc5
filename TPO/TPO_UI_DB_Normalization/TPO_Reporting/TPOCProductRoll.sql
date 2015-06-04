CREATE TABLE [dbo].[TPOCProductRoll]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ProductID] INT NOT NULL, 
    [WorkOrderID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
	[MasterRollID] INT NULL,
    [LengthUoMID] INT NOT NULL, 
    [WeightUoMID] INT NOT NULL, 
    [Code] NVARCHAR(15) NOT NULL, 
    [ProductionDate] DATE NOT NULL, 
    [Length] FLOAT NOT NULL, 
    [Weight] FLOAT NOT NULL, 
    [BatchNumber] INT NOT NULL, 
    [Comments] NVARCHAR(MAX) NULL, 
    [Modified] BIT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOCProductRoll_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_TPOCProductRoll_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [WorkOrder]([ID]),
	CONSTRAINT [FK_TPOCProductRoll_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]),
	CONSTRAINT [FK_TPOCProductRoll_LengthUnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_TPOCProductRoll_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_TPOCProductRoll_MasterRollID] FOREIGN KEY ([MasterRollID]) REFERENCES [TPOCProductRoll]([ID])

)

GO

CREATE UNIQUE INDEX [IX_TPOCProductRoll_Code] ON [dbo].[TPOCProductRoll] ([Code])
