CREATE TABLE [dbo].[WorkOrder] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [LineID]       INT            NOT NULL,
    [PlantID]      INT            NOT NULL,
    [Code]         NVARCHAR (10)  NOT NULL,
    [RunOrder]     INT            NOT NULL,
    [RunArea]      FLOAT (53)     NOT NULL,
    [Pal]          BIT            NOT NULL,
    [Complete]     BIT            NOT NULL,
    [Comment]      NVARCHAR (MAX) NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    [TPOProductID] INT            NULL,
    [IMProductID]  INT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WorkOrder_IMProducts] FOREIGN KEY ([IMProductID]) REFERENCES [dbo].[IMProducts] ([ID]),
    CONSTRAINT [FK_WorkOrder_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_WorkOrder_ProdLines] FOREIGN KEY ([LineID]) REFERENCES [dbo].[ProdLines] ([ID]),
    CONSTRAINT [FK_WorkOrder_TPOProducts] FOREIGN KEY ([TPOProductID]) REFERENCES [dbo].[TPOProducts] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_WorkOrder_Code]
    ON [dbo].[WorkOrder]([Code] ASC);

