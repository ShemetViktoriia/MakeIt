CREATE TABLE [dbo].[Task] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Title]          NVARCHAR (100) NOT NULL,
    [Description]    NVARCHAR (500) NULL,
    [DueDate]        DATETIME       NOT NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [CreatedBy]      NVARCHAR (MAX) NULL,
    [UpdatedDate]    DATETIME       NOT NULL,
    [UpdatedBy]      NVARCHAR (MAX) NULL,
    [AssignedUserId] INT            NULL,
    [CreatedUserId]  INT            NOT NULL,
    [MilestoneId]    INT            NULL,
    [PriorityId]     INT            NOT NULL,
    [ProjectId]      INT            NOT NULL,
    [StatusId]       INT            NOT NULL,
    CONSTRAINT [PK_dbo.Task] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Task_dbo.Milestone_MilestoneId] FOREIGN KEY ([MilestoneId]) REFERENCES [dbo].[Milestone] ([Id]),
    CONSTRAINT [FK_dbo.Task_dbo.Priority_PriorityId] FOREIGN KEY ([PriorityId]) REFERENCES [dbo].[Priority] ([Id]),
    CONSTRAINT [FK_dbo.Task_dbo.Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
    CONSTRAINT [FK_dbo.Task_dbo.Status_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id]),
    CONSTRAINT [FK_dbo.Task_dbo.User_AssignedUserId] FOREIGN KEY ([AssignedUserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_dbo.Task_dbo.User_CreatedUserId] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AssignedUserId]
    ON [dbo].[Task]([AssignedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CreatedUserId]
    ON [dbo].[Task]([CreatedUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MilestoneId]
    ON [dbo].[Task]([MilestoneId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PriorityId]
    ON [dbo].[Task]([PriorityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ProjectId]
    ON [dbo].[Task]([ProjectId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StatusId]
    ON [dbo].[Task]([StatusId] ASC);

