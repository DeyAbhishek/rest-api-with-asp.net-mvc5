CREATE TABLE [dbo].[QCPrintSpecification] (
    [PlantID]      INT            NOT NULL,
    [TypeID]       INT            NOT NULL,
    [Min]          FLOAT (53)     NOT NULL,
    [Max]          FLOAT (53)     NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([PlantID] ASC, [TypeID] ASC),
    CONSTRAINT [FK_QCPrintSpecification_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_QCPrintSpecification_QCPrintSpecificationType] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[QCPrintSpecificationType] ([ID])
);

