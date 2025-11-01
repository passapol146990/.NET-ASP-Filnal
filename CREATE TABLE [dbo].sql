CREATE TABLE [dbo].[Users] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [name]     NVARCHAR (MAX) NULL,
    [email]    NVARCHAR (MAX) NULL,
    [password] NVARCHAR (MAX) NULL,
    [imageUrl] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

