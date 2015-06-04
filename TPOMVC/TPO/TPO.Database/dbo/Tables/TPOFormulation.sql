CREATE TABLE [dbo].[TPOFormulation] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [Description]  NVARCHAR (50) NULL,
    [PlantID]      INT           NOT NULL,
    [LastModified] DATETIME      NOT NULL,
    [Extruders]    INT           NOT NULL,
    CONSTRAINT [PK_TPOFormulation] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TPOFormulation_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID])
);

