CREATE TABLE [dbo].[RawMaterialReceived]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [RawMaterialID] INT NOT NULL, 
    [LotID] INT NULL, 
	[SizeTypeID] INT NULL,
    [DateReceived] DATE NOT NULL, 
    [ReceivedBy] NVARCHAR(50) NULL, 
    [CoA] NVARCHAR(50) NULL, 
    [QuantityShipped] FLOAT NOT NULL, 
    [QuantityReceived] FLOAT NOT NULL, 
    [QuantityUoMID] INT NOT NULL, 
    [QuantityUsedThisLot] FLOAT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_RawMaterialReceived_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_RawMaterialReceived_WeightUnitOfMeasure] FOREIGN KEY ([QuantityUoMID]) REFERENCES [UnitOfMeasure]([ID])
)
