CREATE TABLE [dbo].[IMProductType] (
    [ID]           INT           NOT NULL,
    [Code]         NVARCHAR (5)  NOT NULL,
    [Description]  NVARCHAR (50) NULL,
    [ThickLabel1]  NVARCHAR (25) NULL,
    [ThickLabel2]  NVARCHAR (25) NULL,
    [LastModified] DATETIME      NOT NULL,
    CONSTRAINT [PK_IMProductType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

