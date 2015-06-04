CREATE TABLE [dbo].[WorkOrderShiftData] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [LineID]          INT            NOT NULL,
    [ProductionDate]  DATETIME       NOT NULL,
    [WorkOrderID]     INT            NULL,
    [ScrimAreaUsed]   FLOAT (53)     NOT NULL,
    [ScrimWeightUsed] FLOAT (53)     NOT NULL,
    [DrainedResin]    FLOAT (53)     NOT NULL,
    [PlantID]         INT            NOT NULL,
    [EnteredBy]       NVARCHAR (100) NOT NULL,
    [ModifiedBy]      NVARCHAR (100) NOT NULL,
    [ShiftID]         INT            NOT NULL,
    [DateEntered]     DATETIME       NOT NULL,
    [LastModified]    DATETIME       NOT NULL,
    CONSTRAINT [PK_WorkOrderShiftData] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WorkOrderShiftData_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_WorkOrderShiftData_ProdLines] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_WorkOrderShiftData_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [dbo].[ProductionShift] ([ID]),
    CONSTRAINT [FK_WorkOrderShiftData_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [dbo].[WorkOrder] ([ID])
);

