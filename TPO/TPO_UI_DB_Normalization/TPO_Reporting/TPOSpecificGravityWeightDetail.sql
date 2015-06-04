CREATE TABLE [dbo].[TPOSpecificGravityWeightDetail]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TPOSpecGravID] INT NOT NULL, 
    [TypeID] INT NOT NULL, 
    [Submerged] BIT NOT NULL, 
    [Value] FLOAT NOT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOSpecificGravityWeightDetail_TPOSpecificGravity] FOREIGN KEY ([TPOSpecGravID]) REFERENCES [TPOSpecificGravity]([ID]), 
    CONSTRAINT [FK_TPOSpecificGravityWeightDetail_TPOSpecificGravityWeightDetailType] FOREIGN KEY ([TypeID]) REFERENCES [TPOSpecificGravityWeightDetailType]([ID])
)
