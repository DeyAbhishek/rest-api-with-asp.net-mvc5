CREATE TABLE [dbo].[TPOProductLabelDetail] (
    [ID]     INT           IDENTITY (1, 1) NOT NULL,
    [Label1] NVARCHAR (50) NULL,
    [Label2] NVARCHAR (50) NULL,
    [Label3] NVARCHAR (50) NULL,
    CONSTRAINT [PK_ProductLabelDetail] PRIMARY KEY CLUSTERED ([ID] ASC)
);

