CREATE TABLE [dbo].[ProductionBudget] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [TypeID]       INT            NOT NULL,
    [PlantID]      INT            NOT NULL,
    [Month]        INT            NOT NULL,
    [Year]         INT            NOT NULL,
    [Budget]       FLOAT (53)     NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProductionBudget_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ProductionBudget_ProductionBudgetType] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[ProductionBudgetType] ([ID])
);

