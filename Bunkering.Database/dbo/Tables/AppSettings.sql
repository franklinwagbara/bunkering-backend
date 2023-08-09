CREATE TABLE [dbo].[AppSettings] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX) NOT NULL,
    [Value] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_AppSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

