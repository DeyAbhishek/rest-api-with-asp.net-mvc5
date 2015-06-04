CREATE TABLE [dbo].[RawMaterialSpecificGravityDetail] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [RawMaterialSpecGravID] INT            NOT NULL,
    [Submerged]             BIT            NOT NULL,
    [Value]                 FLOAT (53)     NOT NULL,
    [Order]                 INT            NOT NULL,
    [DateEntered]           DATETIME       NOT NULL,
    [EnteredBy]             NVARCHAR (100) NOT NULL,
    [LastModified]          DATETIME       NOT NULL,
    [ModifiedBy]            NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RawMaterialSpecificGravityDetail_RawMaterialSpecificGravity] FOREIGN KEY ([RawMaterialSpecGravID]) REFERENCES [dbo].[RawMaterialSpecificGravity] ([ID])
);

