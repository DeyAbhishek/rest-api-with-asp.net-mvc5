CREATE TABLE [dbo].[Comment]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
	[PlantID] INT NOT NULL,
    [LineID] INT NOT NULL, 
    [ShiftID] INT NOT NULL, 
    [WorkOrderID] INT NOT NULL, 
	[ProductionDate] DATE NOT NULL, 
	[Comment] NVARCHAR(MAX) NOT NULL,
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_Comment_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]), 
    CONSTRAINT [FK_Comment_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]),
	CONSTRAINT [FK_Comment_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [WorkOrder]([ID])
)
