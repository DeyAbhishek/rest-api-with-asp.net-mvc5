CREATE TABLE [dbo].[TPOProductCEDetail] (
    [ID]             INT        IDENTITY (1, 1) NOT NULL,
    [UseCELogo]      BIT        NOT NULL,
    [CEForceTestUoM] INT        NULL,
    [CETensMinMD]    FLOAT (53) NULL,
    [CEElongMinMD]   FLOAT (53) NULL,
    [CETensMinTD]    FLOAT (53) NULL,
    [CEElongMinTD]   FLOAT (53) NULL,
    [CETearMinMD]    FLOAT (53) NULL,
    [CETearMinTD]    FLOAT (53) NULL,
    CONSTRAINT [PK_ProductCEDetail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProductCEDetail_UnitOfMeasure] FOREIGN KEY ([CEForceTestUoM]) REFERENCES [dbo].[UnitOfMeasure] ([ID])
);

