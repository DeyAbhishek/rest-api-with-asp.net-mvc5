CREATE TABLE [dbo].[ProdComments] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [LineID]         INT            NOT NULL,
    [ShiftID]        INT            NOT NULL,
    [WorkOrderID]    INT            NOT NULL,
    [ProductionDate] DATE           NOT NULL,
    [Comment]        NVARCHAR (MAX) NOT NULL,
    [DateEntered]    DATETIME       NOT NULL,
    [EnteredBy]      NVARCHAR (100) NOT NULL,
    [LastModified]   DATETIME       NOT NULL,
    [ModifiedBy]     NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_ProdComments] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProdComments_ProdLines] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_ProdComments_ProductionShift] FOREIGN KEY ([ShiftID]) REFERENCES [dbo].[ProductionShift] ([ID]),
    CONSTRAINT [FK_ProdComments_WorkOrder] FOREIGN KEY ([WorkOrderID]) REFERENCES [dbo].[WorkOrder] ([ID])
);

