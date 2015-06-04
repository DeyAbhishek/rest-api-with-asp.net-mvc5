CREATE TABLE [dbo].[SupervisorOnShift]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ShiftID] INT NOT NULL, 
	[SupervisorUserID] INT NOT NULL,
    [ProductionDate] DATE NOT NULL, 
	[DateEntered] DATETIME NOT NULL,
	[EnteredBy] NVARCHAR(100) NOT NULL,
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_SupervisorOnShift_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [ProductionShift]([ID]),
	CONSTRAINT [FK_SupervisorOnShift_User] FOREIGN KEY ([SupervisorUserID]) REFERENCES [User]([ID])
)
