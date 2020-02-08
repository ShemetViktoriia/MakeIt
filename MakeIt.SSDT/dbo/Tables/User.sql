CREATE TABLE [dbo].[User] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [IsFired]              BIT            NOT NULL,
    [CreateDateTime]       DATETIME       NOT NULL,
    [EditDateTime]         DATETIME       NOT NULL,
    [Email]                NVARCHAR (MAX) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (500) NULL,
    [SecurityStamp]        NVARCHAR (500) NULL,
    [PhoneNumber]          NVARCHAR (50)  NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (50) NULL,
    CONSTRAINT [PK_dbo.User_UserID] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_dbo.User_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
);

