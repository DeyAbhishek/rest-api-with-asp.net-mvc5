CREATE TABLE [dbo].[RawMaterialReceivedSizeLimit] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]      INT            NOT NULL,
    [Code]         NVARCHAR (100) NOT NULL,
    [View]         BIT            NOT NULL,
    [LowLimit]     FLOAT (53)     NOT NULL,
    [HighLimit]    FLOAT (53)     NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RawMaterialReceivedSizeLimit_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID])
);

