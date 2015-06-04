CREATE TABLE [dbo].[TPOBatch]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [RawMaterialID] INT NOT NULL, 
    [LotID] INT NOT NULL, 
    [BatchNumber] INT NOT NULL, 
    [FormNumber] INT NOT NULL, 
    [IsScrim] BIT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOBatch_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID])
)
