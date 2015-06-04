CREATE TABLE [dbo].[ProdSupervisorOnShift] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [IDCode]      INT           NOT NULL,
    [LocID]       INT           NOT NULL,
    [LineIDCode]  NVARCHAR (10) NOT NULL,
    [ProdDate]    DATETIME      NOT NULL,
    [ProdShiftID] INT           NOT NULL,
    [Name]        NVARCHAR (50) NULL,
    [DateMod]     DATETIME      NOT NULL,
    CONSTRAINT [PK_ProdSupervisorOnShift] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProdSupervisorOnShift_Plant] FOREIGN KEY ([LocID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ProdSupervisorOnShift_ProductionShift] FOREIGN KEY ([ProdShiftID]) REFERENCES [dbo].[ProductionShift] ([ID])
);

