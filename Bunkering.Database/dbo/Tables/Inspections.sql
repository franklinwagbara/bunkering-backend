CREATE TABLE [dbo].[Inspections] (
    [Id]                                   INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationId]                        INT            NOT NULL,
    [ScheduledBy]                          NVARCHAR (MAX) NOT NULL,
    [IndicationOfSImilarFacilityWithin2km] NVARCHAR (MAX) NOT NULL,
    [SiteDrainage]                         NVARCHAR (MAX) NOT NULL,
    [SietJettyTopographicSurvey]           NVARCHAR (MAX) NOT NULL,
    [InspectionDocument]                   NVARCHAR (MAX) NOT NULL,
    [Comment]                              NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Inspections] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_Inspections_ApplicationId]
    ON [dbo].[Inspections]([ApplicationId] ASC);

