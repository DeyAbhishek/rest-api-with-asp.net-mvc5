CREATE TABLE [dbo].[RawMaterialReceived] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]             INT            NOT NULL,
    [LotNumber]           NVARCHAR (50)  NULL,
    [DateEntered]         DATETIME       NOT NULL,
    [EnteredBy]           NVARCHAR (100) NOT NULL,
    [LastModified]        DATETIME       NOT NULL,
    [ModifiedBy]          NVARCHAR (100) NOT NULL,
    [RawMaterialID]       INT            DEFAULT ((1)) NOT NULL,
    [CoA]                 NVARCHAR (50)  NULL,
    [QuantityShipped]     FLOAT (53)     DEFAULT ((0)) NOT NULL,
    [QuantityReceived]    FLOAT (53)     DEFAULT ((0)) NOT NULL,
    [QuantityNotReceived] FLOAT (53)     DEFAULT ((0)) NOT NULL,
    [QuantityUsedThisLot] FLOAT (53)     DEFAULT ((0)) NOT NULL,
    [ReceivedSizeLimitID] INT            DEFAULT ((1)) NOT NULL,
    [UOMID]               INT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK__RawMater__3214EC273101D642] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RawMaterialReceived_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_RawMaterialReceived_RawMaterialReceivedSizeLimit] FOREIGN KEY ([ReceivedSizeLimitID]) REFERENCES [dbo].[RawMaterialReceivedSizeLimit] ([ID]),
    CONSTRAINT [FK_RawMaterialReceived_RawMaterials] FOREIGN KEY ([RawMaterialID]) REFERENCES [dbo].[RawMaterials] ([ID]),
    CONSTRAINT [FK_RawMaterialReceived_UnitOfMeasure] FOREIGN KEY ([UOMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID])
);

