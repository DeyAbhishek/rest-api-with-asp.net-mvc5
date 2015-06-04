CREATE TABLE [dbo].[RawMaterialSpecificGravityDetail]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RawMaterialSpecGravID] INT NOT NULL, 
    [Submerged] BIT NOT NULL, 
    [Value] FLOAT NOT NULL, 
    [Order] INT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_RawMaterialSpecificGravityDetail_RawMaterialSpecificGravity] FOREIGN KEY ([RawMaterialSpecGravID]) REFERENCES [RawMaterialSpecificGravity]([ID])
)
