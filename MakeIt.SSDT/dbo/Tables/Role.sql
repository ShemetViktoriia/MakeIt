CREATE TABLE [dbo].[Role] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Role_RoleID] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Role_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

