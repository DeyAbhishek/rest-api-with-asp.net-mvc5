CREATE TABLE [dbo].[RawMaterialSpecificGravity]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RawMaterialQCID] INT NOT NULL,
    [LabTechUserID] INT NULL, 
    [DenIso] FLOAT NOT NULL DEFAULT 0.7851,
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL
	CONSTRAINT [FK_RawMaterialSpecificGravity_RawMaterialQC] FOREIGN KEY ([RawMaterialQCID]) REFERENCES [RawMaterialQC]([ID]),
	CONSTRAINT [FK_RawMaterialSpecificGravity_User] FOREIGN KEY ([LabTechUserID]) REFERENCES [User]([ID])
)
