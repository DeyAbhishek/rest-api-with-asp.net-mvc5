CREATE TABLE [dbo].[TPOReclaimWIP] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]      INT            NOT NULL,
    [ReclaimType]  NVARCHAR (32)  NOT NULL,
    [Wip]          FLOAT (53)     NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TPOReclaimWIP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOReclaimWIP_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID])
);

