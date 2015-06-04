CREATE TABLE [dbo].[TPOFormulationLineProduct]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY,
	[FormulationID] INT NOT NULL,
	[PlantID] INT NOT NULL,
	[LineID] INT NOT NULL,
	[ProductID] INT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOFormulationLineProduct_TPOFormulation] FOREIGN KEY ([FormulationID]) REFERENCES [TPOFormulation]([ID]),
	CONSTRAINT [FK_TPOFormulationLineProduct_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID])
)
