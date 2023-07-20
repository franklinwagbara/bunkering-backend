﻿CREATE TABLE [dbo].[SubmittedDocuments] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationId] INT            NOT NULL,
    [FileId]        INT            NOT NULL,
    [DocId]         INT            NOT NULL,
    [DocSource]     NVARCHAR (MAX) NOT NULL,
    [DocType]       NVARCHAR (MAX) NOT NULL,
    [DocName]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_SubmittedDocuments] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_SubmittedDocuments_ApplicationId]
    ON [dbo].[SubmittedDocuments]([ApplicationId] ASC);

