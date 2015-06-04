CREATE TABLE [dbo].[WorkOrderShiftDataFormulation] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [LineID]          INT            NOT NULL,
    [ProductionDate]  DATETIME       NOT NULL,
    [ProductionShift] NVARCHAR (5)   NOT NULL,
    [WorkOrderID]     INT            NULL,
    [Extruder]        NVARCHAR (5)   NOT NULL,
    [Feeder]          INT            NOT NULL,
    [RawMaterialID]   INT            NOT NULL,
    [RawMaterialCode] NVARCHAR (50)  NOT NULL,
    [QuantityUsed]    FLOAT (53)     NOT NULL,
    [PlantID]         INT            NOT NULL,
    [EnteredDate]     DATETIME       NOT NULL,
    [EnteredBy]       NVARCHAR (100) NOT NULL,
    [ModifiedDate]    DATETIME       NOT NULL,
    [ModifiedBy]      NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_WorkOrderShiftDataFormulation] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WorkOrderShiftDataFormulation_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_WorkOrderShiftDataFormulation_ProdLines] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_WorkOrderShiftDataFormulation_RawMaterials] FOREIGN KEY ([RawMaterialID]) REFERENCES [dbo].[RawMaterials] ([ID]),
    CONSTRAINT [FK_WorkOrderShiftDataFormulation_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [dbo].[WorkOrder] ([ID])
);

