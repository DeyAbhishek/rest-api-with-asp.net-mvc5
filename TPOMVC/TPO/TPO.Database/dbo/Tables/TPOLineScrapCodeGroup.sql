CREATE TABLE [dbo].[TPOLineScrapCodeGroup] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]      INT            NOT NULL,
    [Code]         NVARCHAR (50)  NOT NULL,
    [Description]  NVARCHAR (50)  NULL,
    [View]         BIT            NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOLineScrapCodeGroup_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID])
);

