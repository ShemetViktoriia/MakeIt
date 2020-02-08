CREATE TABLE [dbo].[Label] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [CreatedBy]   NVARCHAR (MAX) NULL,
    [UpdatedDate] DATETIME       NOT NULL,
    [UpdatedBy]   NVARCHAR (MAX) NULL,
    [ColorId]     INT            NULL,
    CONSTRAINT [PK_dbo.Label] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Label_dbo.Color_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [dbo].[Color] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ColorId]
    ON [dbo].[Label]([ColorId] ASC);

