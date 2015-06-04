CREATE TABLE [dbo].[TPOExtruder] (
    [ID]        INT          NOT NULL,
    [Code]      NVARCHAR (5) NOT NULL,
    [SortOrder] INT          NOT NULL,
    CONSTRAINT [PK_TPOExtruders] PRIMARY KEY CLUSTERED ([ID] ASC)
);

