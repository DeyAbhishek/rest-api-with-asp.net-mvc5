CREATE TABLE [dbo].[IMQC]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [LineID] INT NOT NULL, 
	[LotID] INT NOT NULL,
    [ProductID] INT NOT NULL, 
	[LabTechUserID] INT NULL, 
    [OperatorUserID] INT NULL, 
    [ProductionDate] DATE NOT NULL, 
    [Color] FLOAT NULL, 
    [Passed] BIT NULL, 
    [Comment] NVARCHAR(MAX) NULL, 
	[DateEntered] DATETIME NOT NULL, 
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_IMQC_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_IMQC_LabTechUserID] FOREIGN KEY ([LabTechUserID]) REFERENCES [User]([ID]),
	CONSTRAINT [FK_IMQC_OperatorUserID] FOREIGN KEY ([OperatorUserID]) REFERENCES [User]([ID])
)
