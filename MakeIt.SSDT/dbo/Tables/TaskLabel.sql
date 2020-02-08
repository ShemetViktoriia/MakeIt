CREATE TABLE [dbo].[TaskLabel] (
    [TaskId]  INT NOT NULL,
    [LabelId] INT NOT NULL,
    CONSTRAINT [PK_dbo.TaskLabel] PRIMARY KEY CLUSTERED ([TaskId] ASC, [LabelId] ASC),
    CONSTRAINT [FK_dbo.TaskLabel_dbo.Label_LabelId] FOREIGN KEY ([LabelId]) REFERENCES [dbo].[Label] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TaskLabel_dbo.Task_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Task] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TaskId]
    ON [dbo].[TaskLabel]([TaskId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LabelId]
    ON [dbo].[TaskLabel]([LabelId] ASC);

