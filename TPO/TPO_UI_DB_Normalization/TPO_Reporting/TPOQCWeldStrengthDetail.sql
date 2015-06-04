CREATE TABLE [dbo].[TPOQCWeldStrengthDetail]
(
	[TPOQCID] INT NOT NULL PRIMARY KEY, 
    [Weld1] FLOAT NULL, 
    [Weld2] FLOAT NULL, 
    [Weld3] FLOAT NULL, 
    [Weld4] FLOAT NULL, 
    [Weld5] FLOAT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOQCWeldStrengthDetail_TPOQC] FOREIGN KEY ([TPOQCID]) REFERENCES [TPOQC]([ID])
)
