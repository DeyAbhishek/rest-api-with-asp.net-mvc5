CREATE TABLE [dbo].[UnitOfMeasureDefault] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [PlantID]      INT           NOT NULL,
    [UoMID]        INT           NOT NULL,
    [UoMTypeID]    INT           NOT NULL,
    [DateEntered]  DATETIME      NOT NULL,
    [EnteredBy]    NVARCHAR (50) NOT NULL,
    [LastModified] DATETIME      NOT NULL,
    [ModifiedBy]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UnitOfMeasureDefault] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UnitOfMeasureDefault_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_UnitOfMeasureDefault_UnitOfMeasure] FOREIGN KEY ([UoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_UnitOfMeasureDefault_UnitOfMeasureType] FOREIGN KEY ([UoMTypeID]) REFERENCES [dbo].[UnitOfMeasureType] ([ID])
);

