CREATE TABLE [dbo].[SecurityTask] (
    [SecurityTaskId] INT            NOT NULL,
    [Name]           NVARCHAR (100) NULL,
    [Active]         BIT            NULL,
    CONSTRAINT [PK_SecurityTask] PRIMARY KEY CLUSTERED ([SecurityTaskId] ASC)
);

