CREATE TABLE [dbo].[ProductionShiftType] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [Code]             NVARCHAR (5)    NOT NULL,
    [Description]      NVARCHAR (2000) NOT NULL,
    [SortOrder]        INT             DEFAULT ((0)) NOT NULL,
    [DateEntered]      DATETIME        NOT NULL,
    [EnteredBy]        NVARCHAR (100)  NOT NULL,
    [LastModified]     DATETIME        NOT NULL,
    [ModifiedBy]       NVARCHAR (100)  NOT NULL,
    [ShortDescription] NVARCHAR (200)  DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductionShiftType_Code]
    ON [dbo].[ProductionShiftType]([Code] ASC);

