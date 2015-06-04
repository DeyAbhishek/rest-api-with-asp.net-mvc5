CREATE TABLE [dbo].[User] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]      INT            NOT NULL,
    [Username]     NVARCHAR (50)  NULL,
    [FullName]     NVARCHAR (100) NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK__User__3214EC27E3FFBD8A] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_User_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID])
);

