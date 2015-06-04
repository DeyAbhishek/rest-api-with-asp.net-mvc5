CREATE TABLE [dbo].[DownTime]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
    [ProductID] INT NOT NULL, 
    [WorkOrderID] INT NOT NULL, 
    [TypeID] INT NOT NULL, 
	[ReasonID] INT NOT NULL,
    [ShiftID] INT NOT NULL, 
	[EquipmentID] INT NULL,
    [DateOccurred] DATETIME NOT NULL, 
    [Comment] NVARCHAR(MAX) NULL, 
	[DateEntered] DATETIME NOT NULL, 
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_DownTime_DownTimeType] FOREIGN KEY ([TypeID]) REFERENCES [DownTimeType]([ID]), 
    CONSTRAINT [FK_DownTime_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]), 
    CONSTRAINT [FK_DownTime_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [WorkOrder]([ID]),
	CONSTRAINT [FK_DownTime_DownTimeReason] FOREIGN KEY ([ReasonID]) REFERENCES [DownTimeReason]([ID]),
	CONSTRAINT [FK_DownTime_DownTimeEquipment] FOREIGN KEY ([EquipmentID]) REFERENCES [DownTimeEquipment]([ID]),
	CONSTRAINT [FK_DownTime_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID])
)
