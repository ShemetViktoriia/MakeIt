CREATE TABLE [dbo].[Visitor] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Login] NVARCHAR (MAX) NULL,
    [Ip]    NVARCHAR (MAX) NULL,
    [Url]   NVARCHAR (MAX) NULL,
    [Date]  DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Visitor] PRIMARY KEY CLUSTERED ([Id] ASC)
);

