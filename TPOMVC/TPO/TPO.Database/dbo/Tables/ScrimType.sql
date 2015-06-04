CREATE TABLE [dbo].[ScrimType] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [PlantID]      INT           NOT NULL,
    [WidthUoMID]   INT           NOT NULL,
    [WeightUoMID]  INT           NOT NULL,
    [LengthUoMID]  INT           NOT NULL,
    [Code]         NVARCHAR (25) NOT NULL,
    [Description]  NVARCHAR (50) NOT NULL,
    [Width]        FLOAT (53)    NOT NULL,
    [Length]       FLOAT (53)    NOT NULL,
    [Weight]       FLOAT (53)    NOT NULL,
    [IsLiner]      BIT           NOT NULL,
    [LastModified] DATETIME      NOT NULL,
    [AreaUoMID]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ScrimType_AreaUnitOfMeasure] FOREIGN KEY ([AreaUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_ScrimType_LengthUnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_ScrimType_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ScrimType_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_ScrimType_WidthUnitOfMeasure] FOREIGN KEY ([WidthUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ScrimType_Code]
    ON [dbo].[ScrimType]([Code] ASC);

