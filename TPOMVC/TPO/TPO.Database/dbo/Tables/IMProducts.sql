CREATE TABLE [dbo].[IMProducts] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [Code]            NVARCHAR (20) NULL,
    [Description]     NVARCHAR (50) NOT NULL,
    [IMProductTypeID] INT           NOT NULL,
    [PartsPerCarton]  INT           NOT NULL,
    [CartonsPerLot]   INT           NOT NULL,
    [RawMaterialID]   INT           NOT NULL,
    [ThickUOM]        INT           NOT NULL,
    [WeightUOM]       INT           NOT NULL,
    [MinimumThick]    FLOAT (53)    NOT NULL,
    [MinimumWeight]   FLOAT (53)    NOT NULL,
    [Label1]          NVARCHAR (50) NULL,
    [Label2]          NVARCHAR (50) NULL,
    [Label3]          NVARCHAR (50) NULL,
    [LastModified]    DATETIME      NOT NULL,
    [PlantID]         INT           NOT NULL,
    [Active]          BIT           NOT NULL,
    CONSTRAINT [PK_IMProducts] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IMProducts_IMProductType] FOREIGN KEY ([IMProductTypeID]) REFERENCES [dbo].[IMProductType] ([ID]),
    CONSTRAINT [FK_IMProducts_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_IMProducts_RawMaterials] FOREIGN KEY ([RawMaterialID]) REFERENCES [dbo].[RawMaterials] ([ID]),
    CONSTRAINT [FK_IMProducts_UnitOfMeasure] FOREIGN KEY ([ThickUOM]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_IMProducts_UnitOfMeasure1] FOREIGN KEY ([WeightUOM]) REFERENCES [dbo].[UnitOfMeasure] ([ID])
);



