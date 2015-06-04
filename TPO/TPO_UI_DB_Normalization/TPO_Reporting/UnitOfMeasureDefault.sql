CREATE TABLE [dbo].[UnitOfMeasureDefault]
(
	[PlantID] INT NOT NULL,
	[UoMID] INT NOT NULL, 
	[UoMTypeID] INT NOT NULL,
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
	CONSTRAINT [FK_UnitOfMeasureDefault_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
    CONSTRAINT [FK_UnitOfMeasureDefault_UnitOfMeasure] FOREIGN KEY ([UoMID]) REFERENCES [UnitOfMeasure]([ID]), 
	CONSTRAINT [FK_UnitOfMeasureDefault_UnitOfMeasureType] FOREIGN KEY ([UoMID]) REFERENCES [UnitOfMeasureType]([ID]),
    CONSTRAINT [PK_UnitOfMeasureDefault] PRIMARY KEY ([UoMTypeID], [PlantID], [UoMID])
)
