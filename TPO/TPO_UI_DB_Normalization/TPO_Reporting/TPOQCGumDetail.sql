CREATE TABLE [dbo].[TPOQCGumDetail]
(
	[TPOQCID] INT NOT NULL PRIMARY KEY, 
    [EastGumWidth] FLOAT NULL, 
    [WestGumWidth] FLOAT NULL, 
    [EastGumPeel] BIT NULL, 
    [WestGumPeel] BIT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOQCGumDetail_TPOQC] FOREIGN KEY ([TPOQCID]) REFERENCES [TPOQC]([ID])
)
