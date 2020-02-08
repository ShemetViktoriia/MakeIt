CREATE TABLE [dbo].[Priority] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (20)  NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [CreatedBy]   NVARCHAR (MAX) NULL,
    [UpdatedDate] DATETIME       NOT NULL,
    [UpdatedBy]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Priority] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[Priority]([Name] ASC);

