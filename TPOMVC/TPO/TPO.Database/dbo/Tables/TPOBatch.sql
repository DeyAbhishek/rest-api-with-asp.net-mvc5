CREATE TABLE [dbo].[TPOBatch] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]               INT            NOT NULL,
    [LineID]                INT            NOT NULL,
    [RawMaterialID]         INT            NULL,
    [RawMaterialReceivedID] INT            NULL,
    [BatchNumber]           INT            NOT NULL,
    [FormulationID]         INT            NOT NULL,
    [IsScrim]               BIT            NOT NULL,
    [DateEntered]           DATETIME       NOT NULL,
    [EnteredBy]             NVARCHAR (100) NOT NULL,
    [LastModified]          DATETIME       NOT NULL,
    [ModifiedBy]            NVARCHAR (100) NOT NULL,
    [ScrimRollID]           INT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOBatch_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_TPOBatch_ProdLines] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_TPOBatch_RawMaterialReceived] FOREIGN KEY ([RawMaterialReceivedID]) REFERENCES [dbo].[RawMaterialReceived] ([ID]),
    CONSTRAINT [FK_TPOBatch_RawMaterials] FOREIGN KEY ([RawMaterialID]) REFERENCES [dbo].[RawMaterials] ([ID]),
    CONSTRAINT [FK_TPOBatch_ScrimRoll] FOREIGN KEY ([ScrimRollID]) REFERENCES [dbo].[ScrimRoll] ([ID]),
    CONSTRAINT [FK_TPOBatch_TPOFormulation] FOREIGN KEY ([FormulationID]) REFERENCES [dbo].[TPOFormulation] ([ID])
);



