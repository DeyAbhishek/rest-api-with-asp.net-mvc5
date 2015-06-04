CREATE TABLE [dbo].[TPOProductDimDetail] (
    [ID]             INT        IDENTITY (1, 1) NOT NULL,
    [DimStab80]      FLOAT (53) NULL,
    [DimStabTemp]    FLOAT (53) NULL,
    [DimStabTempCE]  FLOAT (53) NULL,
    [DimStabTempUoM] INT        NOT NULL,
    CONSTRAINT [PK_ProductDimDetail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProductDimDetail_UnitOfMeasure] FOREIGN KEY ([DimStabTempUoM]) REFERENCES [dbo].[UnitOfMeasure] ([ID])
);

