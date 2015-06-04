CREATE TABLE [dbo].[TPORawMaterialFormulation]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [FormulationID] INT NOT NULL, 
    [RawMaterialID] INT NOT NULL, 
    [TPOExtruderID] INT NOT NULL, 
    [FeederNumber] INT NOT NULL, 
    [PercentComplete] FLOAT NOT NULL, 
    [Delete] BIT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPORawMaterialFormulation_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_TPORawMaterialFormulation_TPOFormulation] FOREIGN KEY ([FormulationID]) REFERENCES [TPOFormulation]([ID]),
	CONSTRAINT [FK_TPORawMaterialFormulation_TPOExtruder] FOREIGN KEY ([TPOExtruderID]) REFERENCES [TPOExtruder]([ID])
)
