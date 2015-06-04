CREATE TABLE [dbo].[RoleAssignment] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [UserID]       INT            NOT NULL,
    [RoleID]       INT            NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_RoleAssignment] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RoleAssignment_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([ID]),
    CONSTRAINT [FK_RoleAssignment_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

