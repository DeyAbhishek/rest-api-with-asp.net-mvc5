CREATE TABLE [dbo].[UnitOfMeasureConversion]
(
	[UoMID1] INT NOT NULL,
	[UoMID2] INT NOT NULL,
	[ConversionRate] FLOAT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_AreaUnitOfMeasureConversion] PRIMARY KEY ([UoMID1], [UoMID2]), 
    CONSTRAINT [FK_AreaUnitOfMeasureConversion_UnitOfMeasure1] FOREIGN KEY ([UoMID1]) REFERENCES [UnitOfMeasure]([ID]),
	CONSTRAINT [FK_AreaUnitOfMeasureConversion_UnitOfMeasure2] FOREIGN KEY ([UoMID2]) REFERENCES [UnitOfMeasure]([ID])
)
