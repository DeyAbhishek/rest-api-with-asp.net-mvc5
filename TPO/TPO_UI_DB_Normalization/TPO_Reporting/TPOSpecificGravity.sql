CREATE TABLE [dbo].[TPOSpecificGravity]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlantID] INT NOT NULL, 
    [RawMaterialID] INT NOT NULL, 
    [RollID] INT NOT NULL, 
    [QCTechUserID] INT NULL, 
    [DenIso] FLOAT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOSpecificGravity_Plant] FOREIGN KEY ([PlantID]) REFERENCES [Plant]([ID]),
	CONSTRAINT [FK_TPOSpecificGravity_TPOCProductRoll] FOREIGN KEY ([RollID]) REFERENCES [TPOCProductRoll]([ID]),
	CONSTRAINT [FK_TPOSpecificGravity_User] FOREIGN KEY ([QCTechUserID]) REFERENCES [User]([ID])
)
