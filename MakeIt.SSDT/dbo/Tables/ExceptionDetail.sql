CREATE TABLE [dbo].[ExceptionDetail] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ExceptionMessage] NVARCHAR (MAX) NULL,
    [ControllerName]   NVARCHAR (MAX) NULL,
    [ActionName]       NVARCHAR (MAX) NULL,
    [StackTrace]       NVARCHAR (MAX) NULL,
    [Date]             DATETIME       NOT NULL,
    [CreatedDate]      DATETIME       NOT NULL,
    [CreatedBy]        NVARCHAR (MAX) NULL,
    [UpdatedDate]      DATETIME       NOT NULL,
    [UpdatedBy]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.ExceptionDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);

