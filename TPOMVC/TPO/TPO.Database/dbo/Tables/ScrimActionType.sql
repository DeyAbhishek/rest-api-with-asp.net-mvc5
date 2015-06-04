CREATE TABLE [dbo].[ScrimActionType] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]      INT            NOT NULL,
    [Code]         NVARCHAR (5)   NOT NULL,
    [Description]  NVARCHAR (100) NOT NULL,
    [SortOrder]    INT            CONSTRAINT [DF__ScrimActi__SortO__534D60F1] DEFAULT ((0)) NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK__ScrimAct__3214EC2749ADD1CD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ScrimActionType_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_ScrimActionType_Code]
    ON [dbo].[ScrimActionType]([Code] ASC);

