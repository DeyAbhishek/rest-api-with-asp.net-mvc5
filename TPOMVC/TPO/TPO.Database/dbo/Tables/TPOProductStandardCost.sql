CREATE TABLE [dbo].[TPOProductStandardCost] (
    [ID]                    INT           IDENTITY (1, 1) NOT NULL,
    [PlantID]               INT           NOT NULL,
    [TPOProductID]          INT           NOT NULL,
    [StandardWeightPerArea] FLOAT (53)    NOT NULL,
    [StartDate]             DATE          NOT NULL,
    [EndDate]               DATE          NOT NULL,
    [LastModified]          DATETIME      NOT NULL,
    [StandardCost]          FLOAT (53)    NOT NULL,
    [DateEntered]           DATETIME      NOT NULL,
    [EnteredBy]             NVARCHAR (50) NOT NULL,
    [ModifiedBy]            NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TPOProductStandartCost] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOProductStandartCost_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_TPOProductStandartCost_TPOProducts] FOREIGN KEY ([TPOProductID]) REFERENCES [dbo].[TPOProducts] ([ID])
);

