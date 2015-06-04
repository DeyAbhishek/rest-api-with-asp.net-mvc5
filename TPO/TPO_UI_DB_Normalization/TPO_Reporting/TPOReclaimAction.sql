CREATE TABLE [dbo].[TPOReclaimAction]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [TypeID] INT NOT NULL, 
    [ReclaimTypeID] INT NOT NULL, 
	[TPOLineScrapID] INT NOT NULL,
    [ProductionDate] DATE NOT NULL, 
    [ActionAmount] FLOAT NOT NULL, 
    [CompAmount] FLOAT NOT NULL, 
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
	[ModifiedBy] NVARCHAR(100) NOT NULL,
    CONSTRAINT [FK_TPOReclaimAction_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]), 
    CONSTRAINT [FK_TPOReclaimAction_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]), 
    CONSTRAINT [FK_TPOReclaimAction_TPOReclaimActionType] FOREIGN KEY ([TypeID]) REFERENCES [TPOReclaimActionType]([ID]), 
    CONSTRAINT [FK_TPOReclaimAction_ReclaimType] FOREIGN KEY ([ReclaimTypeID]) REFERENCES [ReclaimType]([ID]), 
    CONSTRAINT [FK_TPOReclaimAction_TPOLineScrap] FOREIGN KEY ([TPOLineScrapID]) REFERENCES [TPOLineScrap]([ID])
)
