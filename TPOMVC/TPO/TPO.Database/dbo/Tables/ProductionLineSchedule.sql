CREATE TABLE [dbo].[ProductionLineSchedule] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]          INT            NOT NULL,
    [LineID]           INT            NOT NULL,
    [ShiftID]          INT            NOT NULL,
    [ProductionDate]   DATE           NOT NULL,
    [MinutesScheduled] INT            DEFAULT ((0)) NOT NULL,
    [DateEntered]      DATETIME       NOT NULL,
    [EnteredBy]        NVARCHAR (100) NOT NULL,
    [LastModified]     DATETIME       NOT NULL,
    [ModifiedBy]       NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProductionLineSchedule_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ProductionLineSchedule_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [dbo].[ProductionShift] ([ID])
);

