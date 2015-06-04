CREATE TABLE [dbo].[TPOFormulationRawMaterials] (
    [ID]               INT        IDENTITY (1, 1) NOT NULL,
    [TPOExtruderID]    INT        NOT NULL,
    [FeedNumber]       INT        NOT NULL,
    [RawMaterialID]    INT        NOT NULL,
    [CompPerc]         FLOAT (53) NOT NULL,
    [PlantID]          INT        NOT NULL,
    [LastModified]     DATETIME   NOT NULL,
    [TPOFormulationID] INT        DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TPOFormRawMaterials] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOFormRawMaterials_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_TPOFormRawMaterials_RawMaterials] FOREIGN KEY ([RawMaterialID]) REFERENCES [dbo].[RawMaterials] ([ID]),
    CONSTRAINT [FK_TPOFormRawMaterials_TPOExtruder] FOREIGN KEY ([TPOExtruderID]) REFERENCES [dbo].[TPOExtruder] ([ID]),
    CONSTRAINT [FK_TPOFormulationRawMaterials_TPOFormulation] FOREIGN KEY ([TPOFormulationID]) REFERENCES [dbo].[TPOFormulation] ([ID])
);

