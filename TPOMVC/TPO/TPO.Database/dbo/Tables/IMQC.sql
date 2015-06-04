CREATE TABLE [dbo].[IMQC] (
    [ID]            INT            NOT NULL,
    [Code]          NVARCHAR (15)  NOT NULL,
    [ProdLineID]    INT            NOT NULL,
    [IMProductID]   INT            NOT NULL,
    [DateEntered]   DATETIME       NOT NULL,
    [ProdDate]      DATE           NOT NULL,
    [LabTechnician] NVARCHAR (50)  NULL,
    [Operator]      NVARCHAR (50)  NULL,
    [Color]         FLOAT (53)     NULL,
    [PassFail]      NVARCHAR (10)  NULL,
    [Comment]       NVARCHAR (500) NULL,
    [PlantID]       INT            NOT NULL,
    [LastModified]  DATETIME       NOT NULL,
    CONSTRAINT [PK_IMQC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IMQC_IMProducts] FOREIGN KEY ([IMProductID]) REFERENCES [dbo].[IMProducts] ([ID]),
    CONSTRAINT [FK_IMQC_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_IMQC_ProdLines] FOREIGN KEY ([ProdLineID]) REFERENCES [dbo].[ProdLines] ([ID])
);

