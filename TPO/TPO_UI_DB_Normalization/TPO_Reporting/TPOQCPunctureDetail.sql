CREATE TABLE [dbo].[TPOQCPunctureDetail]
(
	[TPOQCID] INT NOT NULL PRIMARY KEY, 
    [Puncture1] FLOAT NULL, 
    [Puncture2] FLOAT NULL, 
    [Puncture3] FLOAT NULL, 
    [Puncture4] FLOAT NULL, 
    [Puncture5] NCHAR(10) NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOQCPunctureDetail_TPOQC] FOREIGN KEY ([TPOQCID]) REFERENCES [TPOQC]([ID])
)
