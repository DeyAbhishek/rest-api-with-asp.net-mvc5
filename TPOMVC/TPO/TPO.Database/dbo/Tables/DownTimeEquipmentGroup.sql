CREATE TABLE [dbo].[DownTimeEquipmentGroup] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [LineID]         INT            NOT NULL,
    [PlantID]        INT            NOT NULL,
    [DownTimeTypeID] INT            NOT NULL,
    [Code]           NCHAR (10)     NOT NULL,
    [Description]    NCHAR (100)    NULL,
    [DateEntered]    DATETIME       NOT NULL,
    [EnteredBy]      NVARCHAR (100) NOT NULL,
    [LastModified]   DATETIME       NOT NULL,
    [ModifiedBy]     NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DownTimeEquipmentGroup_DownTimeType] FOREIGN KEY ([DownTimeTypeID]) REFERENCES [dbo].[DownTimeType] ([ID]),
    CONSTRAINT [FK_DownTimeEquipmentGroup_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_DownTimeEquipmentGroup_ProdLine] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID])
);

