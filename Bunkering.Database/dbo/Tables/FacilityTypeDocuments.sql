CREATE TABLE [dbo].[FacilityTypeDocuments] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [DocumentTypeId]    INT            NOT NULL,
    [ApplicationTypeId] INT            NOT NULL,
    [FacilityTypeId]    INT            NOT NULL,
    [Name]              NVARCHAR (MAX) NOT NULL,
    [DocType]           NVARCHAR (MAX) NOT NULL,
    [IsFADDoc]          BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_FacilityTypeDocuments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FacilityTypeDocuments_ApplicationTypes_ApplicationTypeId] FOREIGN KEY ([ApplicationTypeId]) REFERENCES [dbo].[ApplicationTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FacilityTypeDocuments_FacilityTypes_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [dbo].[FacilityTypes] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FacilityTypeDocuments_ApplicationTypeId]
    ON [dbo].[FacilityTypeDocuments]([ApplicationTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FacilityTypeDocuments_FacilityTypeId]
    ON [dbo].[FacilityTypeDocuments]([FacilityTypeId] ASC);

