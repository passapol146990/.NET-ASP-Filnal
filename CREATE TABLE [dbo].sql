CREATE TABLE [dbo].[Users] (
    [Id]       INT  IDENTITY (1, 1) NOT NULL,
    [name]     TEXT NULL,
    [email]    TEXT NULL,
    [password] TEXT NULL,
    [imageUrl] TEXT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

