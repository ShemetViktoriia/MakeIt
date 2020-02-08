CREATE TABLE [dbo].[UserRole] (
    [UserID] INT NOT NULL,
    [RoleID] INT NOT NULL,
    CONSTRAINT [PK_UserRole_UserID_RoleID] PRIMARY KEY CLUSTERED ([UserID] ASC, [RoleID] ASC),
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_UserRole] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserRole_UserID]
    ON [dbo].[UserRole] ([UserID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_UserRole_RoleID]
    ON [dbo].[UserRole] ([RoleID] ASC);
