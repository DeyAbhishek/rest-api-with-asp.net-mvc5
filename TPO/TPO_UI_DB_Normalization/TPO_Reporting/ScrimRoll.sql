CREATE TABLE [dbo].[ScrimRoll]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] NVARCHAR(25) NOT NULL, 
    [TypeID] INT NULL, 
    [PlantID] INT NOT NULL, 
	[LotID] INT NULL,
	[WovenLotID] INT NULL,
	[WeightUoMID] INT NULL,
	[LengthUoMID] INT  NULL,
	[WovenDate] DATETIME NULL,
	[DateReceived] DATETIME NOT NULL,
    [Length] FLOAT NULL, 
	[Weight] FLOAT NULL,
    [TareWeight] FLOAT NULL, 
    [ReceivedLength] FLOAT NULL, 
    [ReceivedWeight] FLOAT NULL, 
    [ReceivedTareWeight] FLOAT NULL, 
    [LengthUsed] FLOAT NULL, 
    [WeightUsed] FLOAT NULL, 
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
	[ModifiedBy] NVARCHAR(100) NOT NULL,
    CONSTRAINT [FK_ScrimRoll_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]), 
    CONSTRAINT [FK_ScrimRoll_ScrimType] FOREIGN KEY ([TypeID]) REFERENCES [ScrimType]([ID]),
	CONSTRAINT [FK_ScrimRoll_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_ScrimRoll_LengthUnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [UnitOfMeasure]([ID])
)

GO

CREATE UNIQUE INDEX [IX_ScrimRoll_Code] ON [dbo].[ScrimRoll] ([Code])
