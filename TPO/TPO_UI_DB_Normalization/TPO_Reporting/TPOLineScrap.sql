CREATE TABLE [dbo].[TPOLineScrap]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
	[PlantID] INT NOT NULL,
    [ShiftID] INT NOT NULL, 
    [WorkOrderID] INT NOT NULL, 
	[ReworkRollID] INT NULL,
	[TypeID] INT NOT NULL,
    [Code] NVARCHAR(15) NOT NULL, 
    [ScrapCode] NVARCHAR(5) NULL, 
    [BatchNumber] INT NOT NULL, 
    [Length] FLOAT NOT NULL, 
    [Width] FLOAT NOT NULL, 
    [Weight] FLOAT NOT NULL, 
    [ProductionDate] DATE NOT NULL, 
    [Comment] NVARCHAR(MAX) NULL, 
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
	[ModifiedBy] NVARCHAR(100) NOT NULL,
    CONSTRAINT [FK_TPOLineScrap_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [WorkOrder]([ID]), 
    CONSTRAINT [FK_TPOLineScrap_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]), 
    CONSTRAINT [FK_TPOLineScrap_TPOLineScrapType] FOREIGN KEY ([TypeID]) REFERENCES [TPOLineScrapType]([ID]),
	CONSTRAINT [FK_TPOLineScrap_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]) ,
	CONSTRAINT [FK_TPOLineScrap_TPOReworkRoll] FOREIGN KEY ([ReworkRollID]) REFERENCES [TPOReworkRoll]([ID]) 
)

GO

CREATE UNIQUE INDEX [IX_LineScrap_Code] ON [dbo].[TPOLineScrap] ([Code])
