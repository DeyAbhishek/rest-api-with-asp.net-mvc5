CREATE TABLE [dbo].[ScrimRoll] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [Code]               NVARCHAR (25)  NOT NULL,
    [TypeID]             INT            NULL,
    [PlantID]            INT            NOT NULL,
    [LotNumber]          INT            NULL,
    [WovenLotNumber]     INT            NULL,
    [WeightUoMID]        INT            NOT NULL,
    [LengthUoMID]        INT            NOT NULL,
    [WovenDate]          DATETIME       NULL,
    [DateReceived]       DATETIME       NOT NULL,
    [Length]             FLOAT (53)     NOT NULL,
    [Weight]             FLOAT (53)     NOT NULL,
    [TareWeight]         FLOAT (53)     NOT NULL,
    [ReceivedLength]     FLOAT (53)     NOT NULL,
    [ReceivedWeight]     FLOAT (53)     NOT NULL,
    [ReceivedTareWeight] FLOAT (53)     NOT NULL,
    [LengthUsed]         FLOAT (53)     NOT NULL,
    [WeightUsed]         FLOAT (53)     NOT NULL,
    [DateEntered]        DATETIME       NOT NULL,
    [EnteredBy]          NVARCHAR (100) NOT NULL,
    [LastModified]       DATETIME       NOT NULL,
    [ModifiedBy]         NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ScrimRoll_LengthUnitOfMeasure] FOREIGN KEY ([LengthUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID]),
    CONSTRAINT [FK_ScrimRoll_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_ScrimRoll_ScrimType] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[ScrimType] ([ID]),
    CONSTRAINT [FK_ScrimRoll_WeightUnitOfMeasure] FOREIGN KEY ([WeightUoMID]) REFERENCES [dbo].[UnitOfMeasure] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ScrimRoll_Code]
    ON [dbo].[ScrimRoll]([Code] ASC);

