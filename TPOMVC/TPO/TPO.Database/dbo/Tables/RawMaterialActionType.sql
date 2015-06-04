CREATE TABLE [dbo].[RawMaterialActionType] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [Code]           NVARCHAR (5)   NOT NULL,
    [Description]    NVARCHAR (50)  NOT NULL,
    [SortOrder]      INT            DEFAULT ((0)) NOT NULL,
    [DateEntered]    DATETIME       NOT NULL,
    [EnteredBy]      NVARCHAR (100) NOT NULL,
    [LastModified]   DATETIME       NOT NULL,
    [LastModifiedBy] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RawMaterialActionType_Code]
    ON [dbo].[RawMaterialActionType]([Code] ASC);

