CREATE TABLE [dbo].[FacilityTypeDocuments] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [DocumentTypeId]    INT            NOT NULL,
    [ApplicationTypeId] INT            NOT NULL,
    [FacilityTypeId]    INT            NOT NULL,
    [IsFADDoc]          BIT            NOT NULL,
    [Name]              NVARCHAR (MAX) NOT NULL,
    [DocType]           NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_FacilityTypeDocuments] PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO
CREATE NONCLUSTERED INDEX [IX_FacilityTypeDocuments_ApplicationTypeId]
    ON [dbo].[FacilityTypeDocuments]([ApplicationTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FacilityTypeDocuments_FacilityTypeId]
    ON [dbo].[FacilityTypeDocuments]([FacilityTypeId] ASC);

