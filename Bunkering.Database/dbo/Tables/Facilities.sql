CREATE TABLE [dbo].[Facilities] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CompanyId]      INT            NOT NULL,
    [ElpsId]         INT            NOT NULL,
    [FacilityTypeId] INT            NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [LgaId]          INT            NOT NULL,
    [Address]        NVARCHAR (MAX) NOT NULL,
    [IsLicensed]     BIT            NOT NULL,
    CONSTRAINT [PK_Facilities] PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO
CREATE NONCLUSTERED INDEX [IX_Facilities_FacilityTypeId]
    ON [dbo].[Facilities]([FacilityTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Facilities_LgaId]
    ON [dbo].[Facilities]([LgaId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Facilities_CompanyId]
    ON [dbo].[Facilities]([CompanyId] ASC);

