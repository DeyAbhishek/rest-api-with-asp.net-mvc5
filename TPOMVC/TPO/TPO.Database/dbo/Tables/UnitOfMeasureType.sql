CREATE TABLE [dbo].[UnitOfMeasureType] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Code]         NVARCHAR (50)  NOT NULL,
    [Description]  NVARCHAR (100) NULL,
    [SortOrder]    INT            DEFAULT ((0)) NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [Enteredby]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

