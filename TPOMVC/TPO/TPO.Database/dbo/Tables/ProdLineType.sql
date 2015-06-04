CREATE TABLE [dbo].[ProdLineType] (
    [ID]               INT           IDENTITY (1, 1) NOT NULL,
    [ProdLineTypeCode] NVARCHAR (5)  NOT NULL,
    [ProdLineTypeDesc] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ProdLineType] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProdLineType_ProdLineType] FOREIGN KEY ([ID]) REFERENCES [dbo].[ProdLineType] ([ID])
);

