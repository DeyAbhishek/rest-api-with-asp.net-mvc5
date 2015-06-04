CREATE TABLE [dbo].[WorkOrder]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LineID] INT NOT NULL, 
    [ProductID] INT NOT NULL, 
    [PlantID] INT NOT NULL, 
    [Code] NVARCHAR(10) NOT NULL, 
    [RunOrder] INT NOT NULL, 
    [RunArea] FLOAT NOT NULL, 
    [Pal] BIT NOT NULL, 
    [Complete] BIT NOT NULL, 
    [Comment] NVARCHAR(MAX) NULL,
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
	[ModifiedBy] NVARCHAR(100) NOT NULL,
    CONSTRAINT [FK_WorkOrder_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID])
)

GO

CREATE UNIQUE INDEX [IX_WorkOrder_Code] ON [dbo].[WorkOrder] ([Code])
