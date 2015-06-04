CREATE TABLE [dbo].[TPOCProductRoll] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]        INT            NOT NULL,
    [LineID]         INT            NOT NULL,
    [ProductID]      INT            NOT NULL,
    [WorkOrderID]    INT            NOT NULL,
    [ShiftID]        INT            NOT NULL,
    [MasterRollID]   INT            NULL,
    [LengthUoMID]    INT            NOT NULL,
    [WeightUoMID]    INT            NOT NULL,
    [Code]           NVARCHAR (15)  NOT NULL,
    [ProductionDate] DATE           NOT NULL,
    [Length]         FLOAT (53)     NOT NULL,
    [Weight]         FLOAT (53)     NOT NULL,
    [BatchNumber]    INT            NOT NULL,
    [Comments]       NVARCHAR (MAX) NULL,
    [Modified]       BIT            NOT NULL,
    [DateEntered]    DATETIME       NOT NULL,
    [EnteredBy]      NVARCHAR (100) NOT NULL,
    [LastModified]   DATETIME       NOT NULL,
    [ModifiedBy]     NVARCHAR (100) NOT NULL,
    [BatchID]        INT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOCProductRoll_LengthUnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_TPOCProductRoll_MasterRollID] FOREIGN KEY ([MasterRollID]) REFERENCES [dbo].[TPOCProductRoll] ([ID]),
    CONSTRAINT [FK_TPOCProductRoll_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_TPOCProductRoll_ProdLines] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_TPOCProductRoll_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [dbo].[ProductionShift] ([ID]),
    CONSTRAINT [FK_TPOCProductRoll_TPOBatch] FOREIGN KEY ([BatchID]) REFERENCES [dbo].[TPOBatch] ([ID]),
    CONSTRAINT [FK_TPOCProductRoll_TPOProducts] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[TPOProducts] ([ID]),
    CONSTRAINT [FK_TPOCProductRoll_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_TPOCProductRoll_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [dbo].[WorkOrder] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TPOCProductRoll_Code]
    ON [dbo].[TPOCProductRoll]([Code] ASC);

