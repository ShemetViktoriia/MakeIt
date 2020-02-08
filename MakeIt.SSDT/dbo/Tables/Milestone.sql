CREATE TABLE [dbo].[Milestone] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [StartDate]   DATETIME       NULL,
    [DueDate]     DATETIME       NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [CreatedBy]   NVARCHAR (MAX) NULL,
    [UpdatedDate] DATETIME       NOT NULL,
    [UpdatedBy]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Milestone] PRIMARY KEY CLUSTERED ([Id] ASC)
);

