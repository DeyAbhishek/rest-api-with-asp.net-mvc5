CREATE TABLE [dbo].[DownTimeType] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Code]         NVARCHAR (5)   NOT NULL,
    [Description]  NVARCHAR (20)  NOT NULL,
    [SortOrder]    INT            DEFAULT ((0)) NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

