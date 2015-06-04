CREATE TABLE [dbo].[TPOFormulationLineProduct] (
    [ID]               INT      NOT NULL,
    [ProdLineID]       INT      NOT NULL,
    [TPOProductID]     INT      NOT NULL,
    [PlantID]          INT      NOT NULL,
    [LastModified]     DATETIME NOT NULL,
    [TPOFormulationID] INT      DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TPOFormLineProd] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOFormLineProd_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_TPOFormLineProd_ProdLines] FOREIGN KEY ([ProdLineID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_TPOFormLineProd_TPOProducts] FOREIGN KEY ([TPOProductID]) REFERENCES [dbo].[TPOProducts] ([ID]),
    CONSTRAINT [FK_TPOFormLineProduct_TPOFormulation] FOREIGN KEY ([TPOFormulationID]) REFERENCES [dbo].[TPOFormulation] ([ID])
);

