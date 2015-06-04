CREATE TABLE [dbo].[TPOReclaimActionType] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Code]         NVARCHAR (2)   NOT NULL,
    [Description]  NVARCHAR (50)  NOT NULL,
    [SortOrder]    INT            NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TPOReclaimActionType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

