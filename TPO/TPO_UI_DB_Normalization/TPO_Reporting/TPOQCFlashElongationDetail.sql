CREATE TABLE [dbo].[TPOQCFlashElongationDetail]
(
	[TPOQCID] INT NOT NULL PRIMARY KEY, 
	[ForceUoMID] INT NOT NULL,
    [Machine1] FLOAT NULL, 
    [Machine2] FLOAT NULL, 
    [Machine3] FLOAT NULL, 
    [Machine4] FLOAT NULL, 
    [Machine5] FLOAT NULL, 
    [Transverse1] FLOAT NULL, 
    [Transverse2] FLOAT NULL, 
    [Transverse3] FLOAT NULL, 
    [Transverse4] FLOAT NULL, 
    [Transverse5] FLOAT NULL, 
    [DateEntered] DATETIME NOT NULL, 
    [EnteredBy] NVARCHAR(100) NOT NULL, 
    [LastModified] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_TPOQCFlashElongationDetail_TPOQC] FOREIGN KEY ([TPOQCID]) REFERENCES [TPOQC]([ID]),
	CONSTRAINT [FK_TPOQCFlashElongationDetail_UnitOfMeasure] FOREIGN KEY ([ForceUoMID]) REFERENCES [UnitOfMeasure]([ID])
)
