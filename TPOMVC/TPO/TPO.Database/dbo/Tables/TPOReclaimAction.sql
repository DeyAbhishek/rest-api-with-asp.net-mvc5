CREATE TABLE [dbo].[TPOReclaimAction] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]             INT            NOT NULL,
    [ProductionDate]      DATE           NOT NULL,
    [ProductionShiftId]   INT            NOT NULL,
    [ProdLineId]          INT            NOT NULL,
    [ReclaimActionTypeId] INT            NOT NULL,
    [ActionAmount]        FLOAT (53)     DEFAULT ((0)) NOT NULL,
    [CompAmount]          FLOAT (53)     DEFAULT ((0)) NOT NULL,
    [AssocAction]         NVARCHAR (50)  NOT NULL,
    [ReclaimType]         NVARCHAR (10)  NOT NULL,
    [DateEntered]         DATETIME       NOT NULL,
    [EnteredBy]           NVARCHAR (100) NOT NULL,
    [LastModified]        DATETIME       NOT NULL,
    [ModifiedBy]          NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TPOReclaimAction] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOReclaimAction_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_TPOReclaimAction_ProdLines] FOREIGN KEY ([ProdLineId]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_TPOReclaimAction_ProductionShift] FOREIGN KEY ([ProductionShiftId]) REFERENCES [dbo].[ProductionShift] ([ID]),
    CONSTRAINT [FK_TPOReclaimAction_TPOReclaimActionType] FOREIGN KEY ([ReclaimActionTypeId]) REFERENCES [dbo].[TPOReclaimActionType] ([ID])
);

