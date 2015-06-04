CREATE TABLE [dbo].[Role] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [RoleName]     NVARCHAR (50)  NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([ID] ASC)
);

