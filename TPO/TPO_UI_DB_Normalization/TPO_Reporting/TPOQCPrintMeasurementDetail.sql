CREATE TABLE [dbo].[TPOQCPrintMeasurementDetail]
(
	[TPOQCID] INT NOT NULL PRIMARY KEY, 
    [TackLine] FLOAT NULL, 
    [LapLine] FLOAT NULL, 
    [WeldLine] FLOAT NULL, 
    [DateLine] FLOAT NULL, 
    [WeldToLapLine] FLOAT NULL, 
    [Center] FLOAT NULL, 
    [TackLineA] FLOAT NULL, 
    [LapLineA] FLOAT NULL, 
    [WeldLineA] FLOAT NULL, 
    [DateLineA] FLOAT NULL, 
    [TackLineB] FLOAT NULL, 
    [LapLineB] FLOAT NULL, 
    [WeldLineB] FLOAT NULL, 
    [DateLineB] FLOAT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOQCPrintMeasurementDetail_TPOQC] FOREIGN KEY ([TPOQCID]) REFERENCES [TPOQC]([ID])
)
