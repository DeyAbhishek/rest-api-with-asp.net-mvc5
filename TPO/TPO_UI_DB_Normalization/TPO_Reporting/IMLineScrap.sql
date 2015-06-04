CREATE TABLE [dbo].[IMLineScrap]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [ProductID] INT NOT NULL, 
    [LotID] INT NOT NULL, 
	[WorkOrderID] INT NOT NULL,
    [ScrapTypeID] INT NOT NULL, 
	[ReasonID] INT NOT NULL,
    [Code] NVARCHAR(15) NOT NULL,
	[BatchNumber] INT NOT NULL,
	[Parts] INT NOT NULL,
	[Weight] FLOAT NOT NULL,
	[WeightUoMID] INT NOT NULL,
	[Comment] NVARCHAR(MAX) NOT NULL,
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
	[LastModified] DATETIME NOT NULL,
	[ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_IMLineScrap_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_IMLineScrap_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]),
	CONSTRAINT [FK_IMLineScrap_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [WorkOrder]([ID]),
	CONSTRAINT [FK_IMLineScrap_ScrapType] FOREIGN KEY ([ScrapTypeID]) REFERENCES [IMLineScrapType]([ID]),
	CONSTRAINT [FK_IMLineScrap_ScrapReason] FOREIGN KEY ([ReasonID]) REFERENCES [IMLineScrapReason]([ID]),
	CONSTRAINT [FK_IMLineScrap_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [UnitOfMeasure]([ID])
)

GO

CREATE UNIQUE INDEX [IX_IMLineScrap_Code] ON [dbo].[IMLineScrap] ([Code])
