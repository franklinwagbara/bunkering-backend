CREATE TABLE [dbo].[States] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [Code]      NVARCHAR (MAX) NOT NULL,
    [CountryId] INT            NOT NULL,
    CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO
CREATE NONCLUSTERED INDEX [IX_States_CountryId]
    ON [dbo].[States]([CountryId] ASC);

