CREATE TABLE [dbo].[TPOLineScrap] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]        INT            NOT NULL,
    [ShiftID]        INT            NOT NULL,
    [WorkOrderID]    INT            NOT NULL,
    [ReworkRollID]   INT            NULL,
    [TypeID]         INT            NOT NULL,
    [Code]           NVARCHAR (15)  NOT NULL,
    [ScrapCode]      NVARCHAR (5)   NULL,
    [BatchNumber]    INT            NOT NULL,
    [Length]         FLOAT (53)     NOT NULL,
    [Width]          FLOAT (53)     NOT NULL,
    [Weight]         FLOAT (53)     NOT NULL,
    [ProductionDate] DATE           NOT NULL,
    [Comment]        NVARCHAR (MAX) NULL,
    [DateEntered]    DATETIME       NOT NULL,
    [EnteredBy]      NVARCHAR (100) NOT NULL,
    [LastModified]   DATETIME       NOT NULL,
    [ModifiedBy]     NVARCHAR (100) NOT NULL,
    [LengthUoMID]    INT            DEFAULT ((6)) NOT NULL,
    [WeightUoMID]    INT            DEFAULT ((8)) NOT NULL,
    [WidthUoMID]     INT            DEFAULT ((31)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOLineScrap_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_TPOLineScrap_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [dbo].[ProductionShift] ([ID]),
    CONSTRAINT [FK_TPOLineScrap_TPOLineScrapType] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[TPOLineScrapType] ([ID]),
    CONSTRAINT [FK_TPOLineScrap_TPOReworkRoll] FOREIGN KEY ([ReworkRollID]) REFERENCES [dbo].[TPOReworkRoll] ([ID]),
    CONSTRAINT [FK_TPOLineScrap_UnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_TPOLineScrap_UnitOfMeasure1] FOREIGN KEY ([WeightUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_TPOLineScrap_UnitOfMeasure2] FOREIGN KEY ([WidthUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_TPOLineScrap_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [dbo].[WorkOrder] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LineScrap_Code]
    ON [dbo].[TPOLineScrap]([Code] ASC);

