CREATE TABLE [dbo].[TPOReworkRoll] (
    [ID]           INT            NOT NULL,
    [LineID]       INT            NOT NULL,
    [ShiftID]      INT            NOT NULL,
    [TPOProductID]    INT            NOT NULL,
    [PlantID]      INT            NOT NULL,
    [Code]         NVARCHAR (15)  NOT NULL,
    [Length]       FLOAT (53)     NOT NULL,
    [LengthUoMID]  INT            NOT NULL,
    [Weight]       FLOAT (53)     NOT NULL,
    [WeightUoMID]  INT            NOT NULL,
    [Processed]    BIT            NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ReworkRolls_UnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_ReworkRolls_UnitOfMeasure2] FOREIGN KEY ([WeightUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_TPOReworkRoll_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_TPOReworkRoll_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [dbo].[ProductionShift] ([ID]), 
    CONSTRAINT [FK_TPOReworkRoll_ProdLines] FOREIGN KEY ([LineID]) REFERENCES [ProdLines]([ID]), 
    CONSTRAINT [FK_TPOReworkRoll_TPOProducts] FOREIGN KEY ([TPOProductID]) REFERENCES [TPOProducts]([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TPOReworkRoll_Code]
    ON [dbo].[TPOReworkRoll]([Code] ASC);

