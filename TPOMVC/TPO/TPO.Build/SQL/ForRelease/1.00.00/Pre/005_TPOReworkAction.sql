IF NOT EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TPOReworkActionType')
BEGIN
	CREATE TABLE [dbo].[TPOReworkActionType]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(100) NULL, 
    [SortOrder] INT NOT NULL DEFAULT 0, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL
)
END
GO

IF NOT EXISTS (select * from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TPOReworkAction')
BEGIN
CREATE TABLE [dbo].[TPOReworkAction]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
	[ProductionDate] DATETIME NOT NULL,
    [StartRollID] INT NOT NULL, 
    [StartRoll2ID] INT NULL, 
    [OutputRollID] INT NULL, 
	[OutputScrapID] INT NULL,
    [NewProductID] INT NOT NULL, 
    [TypeID] INT NOT NULL, 
    [WidthUoMID] INT NOT NULL, 
    [LengthUoMID] INT NOT NULL, 
    [WeightUoMID] INT NOT NULL, 
    [Width] FLOAT NOT NULL, 
    [Length] FLOAT NOT NULL, 
    [Weight] FLOAT NOT NULL, 
    [Comments] NVARCHAR(MAX) NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOReworkAction_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_TPOReworkAction_TPOReworkActionType] FOREIGN KEY ([TypeID]) REFERENCES [TPOReworkActionType]([ID]),
	CONSTRAINT [FK_TPOReworkAction_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]),
	CONSTRAINT [FK_TPOReworkAction_WidthUnitOfMeasure] FOREIGN KEY ([WidthUoMID]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_TPOReworkAction_LengthUnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_TPOReworkAction_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_TPOReworkAction_TPOReworkRoll1] FOREIGN KEY ([StartRollID]) REFERENCES [TPOReworkRoll]([ID]),
	CONSTRAINT [FK_TPOReworkAction_TPOReworkRoll2] FOREIGN KEY ([StartRoll2ID]) REFERENCES [TPOReworkRoll]([ID]),
	CONSTRAINT [FK_TPOReworkAction_TPOCProductRoll] FOREIGN KEY ([OutputRollID]) REFERENCES [TPOCProductRoll]([ID]),
	CONSTRAINT [FK_TPOReworkAction_TPOLineScrap] FOREIGN KEY ([OutputScrapID]) REFERENCES [TPOLineScrap]([ID])
)

END
GO