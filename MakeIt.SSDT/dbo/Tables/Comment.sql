CREATE TABLE [dbo].[Comment] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Text]        NVARCHAR (MAX) NOT NULL,
    [WrittenDate] DATETIME       NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [CreatedBy]   NVARCHAR (MAX) NULL,
    [UpdatedDate] DATETIME       NOT NULL,
    [UpdatedBy]   NVARCHAR (MAX) NULL,
    [TaskId]      INT            NOT NULL,
    [User_Id]     INT            NULL,
    CONSTRAINT [PK_dbo.Comment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Comment_dbo.Task_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Task] ([Id]),
    CONSTRAINT [FK_dbo.Comment_dbo.User_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TaskId]
    ON [dbo].[Comment]([TaskId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[Comment]([User_Id] ASC);

