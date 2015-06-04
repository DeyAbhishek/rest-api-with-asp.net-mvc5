CREATE TABLE [dbo].[TPOQCDimensionalStabilityDetail]
(
	[TPOQCID] INT NOT NULL PRIMARY KEY, 
    [Machine100] FLOAT NULL, 
    [Machine80] FLOAT NULL, 
    [Transverse100] FLOAT NULL, 
    [Transverse80] FLOAT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOQCDimensionalStabilityDetail_TPOQC] FOREIGN KEY ([TPOQCID]) REFERENCES [TPOQC]([ID])
)
