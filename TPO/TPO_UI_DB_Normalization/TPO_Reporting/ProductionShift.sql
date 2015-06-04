CREATE TABLE [dbo].[ProductionShift]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [TypeID] INT NOT NULL, 
	[Code] NVARCHAR(5) NOT NULL,
    [StartTime] TIME(0) NOT NULL, 
    [EndTime] TIME(0) NOT NULL, 
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
	[ModifiedBy] NVARCHAR(100) NOT NULL,
    CONSTRAINT [FK_ProductionShift_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]), 
    CONSTRAINT [FK_ProductionShift_ProductionShiftType] FOREIGN KEY ([TypeID]) REFERENCES [ProductionShiftType]([ID])
)

GO

CREATE UNIQUE INDEX [IX_ProductionShift_Code] ON [dbo].[ProductionShift] ([Code])
