CREATE TABLE [dbo].[UnitOfMeasure]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
	[TypeID] INT NOT NULL,
    [Code] NVARCHAR(10) NOT NULL, 
    [Description] NVARCHAR(50) NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_UnitOfMeasure_UnitOfMeasureType] FOREIGN KEY ([TypeID]) REFERENCES [UnitOfMeasureType]([ID]) 
)

GO
