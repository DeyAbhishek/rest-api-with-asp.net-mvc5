CREATE TABLE [dbo].[TPOQCSpecificGravityDetail]
(
	[TPOQCID] INT NOT NULL PRIMARY KEY, 
    [Cap] FLOAT NULL, 
    [Core] FLOAT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOQCSpecificGravityDetail_TPOQC] FOREIGN KEY ([TPOQCID]) REFERENCES [TPOQC]([ID])
)
