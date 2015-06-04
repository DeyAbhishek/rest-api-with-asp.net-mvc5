CREATE TABLE [dbo].[ProdDateChange] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [PlantID]       INT           NOT NULL,
    [ShiftTypeID]   INT           NOT NULL,
    [DateChange]    DATETIME      NOT NULL,
    [RotationStart] DATETIME      NOT NULL,
    [LineID]        INT           NOT NULL,
    [DateEntered]   DATETIME      NOT NULL,
    [EnteredBy]     NVARCHAR (50) NOT NULL,
    [LastModified]  DATETIME      NOT NULL,
    [ModifiedBy]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_ProdDteChng] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProdDateChange_ProductionShiftType] FOREIGN KEY ([ShiftTypeID]) REFERENCES [dbo].[ProductionShiftType] ([ID]),
    CONSTRAINT [FK_ProdDteChng_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ProdDteChng_ProdLines] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID]) ON DELETE CASCADE
);

