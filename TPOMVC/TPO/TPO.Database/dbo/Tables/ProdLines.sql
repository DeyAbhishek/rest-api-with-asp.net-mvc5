CREATE TABLE [dbo].[ProdLines] (
    [ID]                 INT           IDENTITY (1, 1) NOT NULL,
    [LineDescCode]       NVARCHAR (10) NOT NULL,
    [LineDesc]           NVARCHAR (50) NULL,
    [LineTypeID]         INT           NOT NULL,
    [LabelID]            INT           NOT NULL,
    [TPOMorC]            NVARCHAR (3)  NOT NULL,
    [RepOrder]           INT           NOT NULL,
    [RCComp]             NVARCHAR (25) NULL,
    [PlantID]            INT           NOT NULL,
    [LineRollConfigID]   INT           NOT NULL,
    [DateEntered]        DATETIME      NOT NULL,
    [EnteredBy]          NVARCHAR (50) NOT NULL,
    [LastModified]       DATETIME      NOT NULL,
    [ModifiedBy]         NVARCHAR (50) NOT NULL,
    [CurrentWorkOrderID] INT           NULL,
    CONSTRAINT [PK_ProdLines] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ProdLines_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ProdLines_ProdLineRollConfig] FOREIGN KEY ([LineRollConfigID]) REFERENCES [dbo].[ProdLineRollConfig] ([ID]),
    CONSTRAINT [FK_ProdLines_ProdLineType] FOREIGN KEY ([LineTypeID]) REFERENCES [dbo].[ProdLineType] ([ID]),
    CONSTRAINT [FK_ProdLines_WorkOrder] FOREIGN KEY ([CurrentWorkOrderID]) REFERENCES [dbo].[WorkOrder] ([ID])
);

