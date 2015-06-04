CREATE TABLE [dbo].[ProdLinesPerformProd] (
    [ID]         INT        IDENTITY (1, 1) NOT NULL,
    [LocID]      INT        NOT NULL,
    [ProdLineID] INT        NOT NULL,
    [ProductID]  INT        NOT NULL,
    [Throughput] FLOAT (53) NOT NULL,
    [DateChange] DATETIME   NOT NULL,
    CONSTRAINT [PK_ProdLinesPerformProd] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProdLinesPerformProd_Plant] FOREIGN KEY ([LocID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ProdLinesPerformProd_ProdLines] FOREIGN KEY ([ProdLineID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_ProdLinesPerformProd_TPOProducts] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[TPOProducts] ([ID])
);

