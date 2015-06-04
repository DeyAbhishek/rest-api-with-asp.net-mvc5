CREATE TABLE [dbo].[TPOReworkRoll]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [LineID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [ProductID] INT NOT NULL, 
    [PlantID] INT NOT NULL, 
    [Code] NVARCHAR(15) NOT NULL, 
    [Length] FLOAT NOT NULL, 
    [LengthUoMID] INT NOT NULL, 
    [Weight] FLOAT NOT NULL, 
    [WeightUoMID] INT NOT NULL, 
	[Processed] BIT NOT NULL,
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOReworkRoll_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_TPOReworkRoll_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]),
	CONSTRAINT [FK_TPOReworkRoll_LengthUnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_TPOReworkRoll_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [UnitOfMeasure]([ID])
)

GO

CREATE UNIQUE INDEX [IX_TPOReworkRoll_Code] ON [dbo].[TPOReworkRoll] ([Code])
