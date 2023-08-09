CREATE TABLE [dbo].[FacilitySources] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [FacilityTypeId] INT            NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [Address]        NVARCHAR (MAX) NOT NULL,
    [LicenseNumber]  NVARCHAR (MAX) NOT NULL,
    [StateId]        INT            NOT NULL,
    [LgaId]          INT            NOT NULL,
    CONSTRAINT [PK_FacilitySources] PRIMARY KEY CLUSTERED ([Id] ASC)
);

