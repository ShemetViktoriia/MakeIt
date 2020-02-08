CREATE TABLE [dbo].[Color] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (20)  NOT NULL,
    [Code]        NVARCHAR (7)   NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [CreatedBy]   NVARCHAR (MAX) NULL,
    [UpdatedDate] DATETIME       NOT NULL,
    [UpdatedBy]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Color] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[Color]([Name] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Code]
    ON [dbo].[Color]([Code] ASC);

