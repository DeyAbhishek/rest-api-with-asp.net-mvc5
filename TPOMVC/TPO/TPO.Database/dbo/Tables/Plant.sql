CREATE TABLE [dbo].[Plant] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Code]         NVARCHAR (5)   NOT NULL,
    [Name]         NVARCHAR (25)  NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

