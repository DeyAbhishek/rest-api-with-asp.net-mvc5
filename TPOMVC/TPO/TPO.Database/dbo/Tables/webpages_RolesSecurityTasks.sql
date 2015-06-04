CREATE TABLE [dbo].[webpages_RolesSecurityTasks] (
    [RoleId]         INT NOT NULL,
    [SecurityTaskId] INT NOT NULL,
    CONSTRAINT [PK_webpages_RolesSecurityTask] PRIMARY KEY CLUSTERED ([RoleId] ASC, [SecurityTaskId] ASC),
    CONSTRAINT [FK_webpages_RolesSecurityTask_SecurityTask] FOREIGN KEY ([SecurityTaskId]) REFERENCES [dbo].[SecurityTask] ([SecurityTaskId]),
    CONSTRAINT [FK_webpages_RolesSecurityTask_webpages_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId])
);

