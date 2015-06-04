CREATE TABLE [dbo].[ProductionBudget]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TypeID] INT NOT NULL, 
    [PlantID] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [Year] INT NOT NULL, 
    [Budget] FLOAT NOT NULL, 
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_ProductionBudget_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_ProductionBudget_ProductionBudgetType] FOREIGN KEY ([TypeID]) REFERENCES [ProductionBudgetType]([ID])
)
