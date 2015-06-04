CREATE TABLE [dbo].[DownTimeEquipment] (
    [ID]                       INT            IDENTITY (1, 1) NOT NULL,
    [DownTimeTypeID]           INT            NOT NULL,
    [PlantID]                  INT            NOT NULL,
    [LineID]                   INT            NULL,
    [Description]              NVARCHAR (100) NOT NULL,
    [DateEntered]              DATETIME       NOT NULL,
    [EnteredBy]                NVARCHAR (100) NOT NULL,
    [LastModified]             DATETIME       NOT NULL,
    [ModifiedBy]               NVARCHAR (100) NOT NULL,
    [DownTimeEquipmentGroupID] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DownTimeEquipment_DownTimeEquipmentGroup] FOREIGN KEY ([DownTimeEquipmentGroupID]) REFERENCES [dbo].[DownTimeEquipmentGroup] ([ID]),
    CONSTRAINT [FK_DownTimeEquipment_DownTimeType] FOREIGN KEY ([DownTimeTypeID]) REFERENCES [dbo].[DownTimeType] ([ID]),
    CONSTRAINT [FK_DownTimeEquipment_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_DownTimeEquipment_ProdLine] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID])
);

