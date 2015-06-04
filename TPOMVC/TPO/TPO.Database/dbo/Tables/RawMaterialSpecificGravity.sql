CREATE TABLE [dbo].[RawMaterialSpecificGravity] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [RawMaterialQCID] INT            NOT NULL,
    [LabTechUserID]   INT            NULL,
    [DenIso]          FLOAT (53)     DEFAULT ((0.7851)) NOT NULL,
    [DateEntered]     DATETIME       NOT NULL,
    [EnteredBy]       NVARCHAR (100) NOT NULL,
    [LastModified]    DATETIME       NOT NULL,
    [ModifiedBy]      NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RawMaterialSpecificGravity_RawMaterialQC] FOREIGN KEY ([RawMaterialQCID]) REFERENCES [dbo].[RawMaterialQC] ([ID]),
    CONSTRAINT [FK_RawMaterialSpecificGravity_User] FOREIGN KEY ([LabTechUserID]) REFERENCES [dbo].[User] ([ID])
);

