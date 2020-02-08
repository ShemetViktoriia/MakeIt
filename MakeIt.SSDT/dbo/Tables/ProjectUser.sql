CREATE TABLE [dbo].[ProjectUser] (
    [ProjectId] INT NOT NULL,
    [UserId]    INT NOT NULL,
    CONSTRAINT [PK_dbo.ProjectUser] PRIMARY KEY CLUSTERED ([ProjectId] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.ProjectUser_dbo.Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.ProjectUser_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProjectId]
    ON [dbo].[ProjectUser]([ProjectId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[ProjectUser]([UserId] ASC);

