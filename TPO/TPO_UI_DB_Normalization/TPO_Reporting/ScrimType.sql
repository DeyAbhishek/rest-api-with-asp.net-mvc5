CREATE TABLE [dbo].[ScrimType]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
	[PlantID] INT NOT NULL, 
	[WidthUoMID] INT NOT NULL, 
    [WeightUoMID] INT NOT NULL, 
    [LengthUoMID] INT NOT NULL, 
    [Code] NVARCHAR(25) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    [Width] FLOAT NOT NULL, 
    [Length] FLOAT NOT NULL, 
    [Weight] FLOAT NOT NULL, 
    [IsLiner] BIT NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    CONSTRAINT [FK_ScrimType_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]), 
    CONSTRAINT [FK_ScrimType_WidthUnitOfMeasure] FOREIGN KEY ([WidthUoMID]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_ScrimType_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_ScrimType_LengthUnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [UnitOfMeasure]([ID])
	
)

GO

CREATE UNIQUE INDEX [IX_ScrimType_Code] ON [dbo].[ScrimType] ([Code])
