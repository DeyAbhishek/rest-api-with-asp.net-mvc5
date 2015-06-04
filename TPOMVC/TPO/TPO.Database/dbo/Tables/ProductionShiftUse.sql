CREATE TABLE [dbo].[ProductionShiftUse] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]      INT            NOT NULL,
    [LineID]       INT            NOT NULL,
    [ShiftID]      INT            NOT NULL,
    [UseShift]     BIT            NOT NULL,
    [Day1Minutes]  INT            DEFAULT ((0)) NOT NULL,
    [Day2Minutes]  INT            DEFAULT ((0)) NOT NULL,
    [Day3Minutes]  INT            DEFAULT ((0)) NOT NULL,
    [Day4Minutes]  INT            DEFAULT ((0)) NOT NULL,
    [Day5Minutes]  INT            DEFAULT ((0)) NOT NULL,
    [Day6Minutes]  INT            DEFAULT ((0)) NOT NULL,
    [Day7Minutes]  INT            DEFAULT ((0)) NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProductionShiftUse_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ProductionShiftUse_ProdLines] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductionShiftUse_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [dbo].[ProductionShift] ([ID])
);

