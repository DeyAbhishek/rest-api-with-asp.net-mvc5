CREATE TABLE [dbo].[UnitOfMeasureConversion] (
    [UoMID1]         INT            NOT NULL,
    [UoMID2]         INT            NOT NULL,
    [ConversionRate] FLOAT (53)     NOT NULL,
    [DateEntered]    DATETIME       NOT NULL,
    [EnteredBy]      NVARCHAR (100) NOT NULL,
    [LastModified]   DATETIME       NOT NULL,
    [ModifiedBy]     NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_AreaUnitOfMeasureConversion] PRIMARY KEY CLUSTERED ([UoMID1] ASC, [UoMID2] ASC),
    CONSTRAINT [FK_AreaUnitOfMeasureConversion_UnitOfMeasure1] FOREIGN KEY ([UoMID1]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_AreaUnitOfMeasureConversion_UnitOfMeasure2] FOREIGN KEY ([UoMID2]) REFERENCES [dbo].[UnitOfMeasure] ([ID])
);

