CREATE TABLE [dbo].[UserPlant] (
    [UserId]       INT            NOT NULL,
    [PlantId]      INT            NOT NULL,
    [DateEntered]  DATETIME       CONSTRAINT [DF_UserPlant_DateEntered] DEFAULT (getutcdate()) NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       CONSTRAINT [DF_UserPlant_LastModified] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_UserPlant] PRIMARY KEY CLUSTERED ([UserId] ASC, [PlantId] ASC),
    CONSTRAINT [FK_UserPlant_Plant] FOREIGN KEY ([PlantId]) REFERENCES [dbo].[Plant] ([ID]),
    CONSTRAINT [FK_UserPlant_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([ID])
);

