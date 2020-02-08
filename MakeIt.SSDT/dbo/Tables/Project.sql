CREATE TABLE [dbo].[Project] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (20)  NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [CreatedBy]   NVARCHAR (MAX) NULL,
    [UpdatedDate] DATETIME       NOT NULL,
    [UpdatedBy]   NVARCHAR (MAX) NULL,
    [OwnerId]     INT            NOT NULL,
    [IsClosed]    BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Project] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Project_dbo.User_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_OwnerId]
    ON [dbo].[Project]([OwnerId] ASC);

