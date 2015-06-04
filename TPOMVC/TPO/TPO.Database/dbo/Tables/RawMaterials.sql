CREATE TABLE [dbo].[RawMaterials] (
    [ID]                  INT           IDENTITY (1, 1) NOT NULL,
    [Code]                NVARCHAR (25) NOT NULL,
    [Description]         NVARCHAR (50) NOT NULL,
    [PricePerUOM]         FLOAT (53)    NOT NULL,
    [Density]             FLOAT (53)    NOT NULL,
    [FSBPID]              NCHAR (10)    NULL,
    [PlantID]             INT           NOT NULL,
    [LastModified]        DATETIME      NOT NULL,
    [UOMID]               INT           NOT NULL,
    [RawMaterialVendorID] INT           NOT NULL,
    [Stock]               AS            ([dbo].[FetchRawMaterialsStock]([id])),
    CONSTRAINT [PK_RawMaterials] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RawMaterials_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_RawMaterials_RawMaterialVendor] FOREIGN KEY ([RawMaterialVendorID]) REFERENCES [dbo].[RawMaterialVendor] ([ID]),
    CONSTRAINT [FK_RawMaterials_UnitOfMeasure] FOREIGN KEY ([UOMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID])
);

