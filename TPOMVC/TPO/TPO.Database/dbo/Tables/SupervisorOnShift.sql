CREATE TABLE [dbo].[SupervisorOnShift] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]        INT            NOT NULL,
    [LineID]         INT            NOT NULL,
    [ProductionDate] DATE           NOT NULL,
    [ShiftID]        INT            NOT NULL,
    [SupervisorName] NVARCHAR (100) NOT NULL,
    [DateEntered]    DATETIME       NOT NULL,
    [EnteredBy]      NVARCHAR (100) NOT NULL,
    [LastModified]   DATETIME       NOT NULL,
    [ModifiedBy]     NVARCHAR (100) NOT NULL,
    CONSTRAINT [FK_SupervisorOnShift_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_SupervisorOnShift_ProdLines] FOREIGN KEY ([ID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_SupervisorOnShift_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [dbo].[ProductionShift] ([ID]), 
    CONSTRAINT [PK_SupervisorOnShift] PRIMARY KEY ([ID])
);
