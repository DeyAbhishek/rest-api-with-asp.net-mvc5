CREATE TABLE [dbo].[ProdLinesPerform] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [LocID]           INT           NOT NULL,
    [LineCode]        NVARCHAR (10) NOT NULL,
    [Uptime]          FLOAT (53)    NOT NULL,
    [Yield]           FLOAT (53)    NOT NULL,
    [Throughput]      FLOAT (53)    NOT NULL,
    [OEE]             FLOAT (53)    NOT NULL,
    [TPTUse]          NVARCHAR (2)  NOT NULL,
    [SchedFactor]     FLOAT (53)    NOT NULL,
    [ProdLineID]      INT           NOT NULL,
    [DateEntered]     DATETIME      NOT NULL,
    [EnteredBy]       NVARCHAR (50) NOT NULL,
    [LastModified]    DATETIME      NOT NULL,
    [ModifiedBy]      NVARCHAR (50) NOT NULL,
    [ThroughputUoMID] INT           NULL,
    CONSTRAINT [PK_ProdLinesPerform] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProdLinesPerform_Plant] FOREIGN KEY ([LocID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ProdLinesPerform_ProdLines] FOREIGN KEY ([ProdLineID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_ProdLinesPerform_UnitOfMeasure] FOREIGN KEY ([ThroughputUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID])
);

