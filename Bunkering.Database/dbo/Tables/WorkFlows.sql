CREATE TABLE [dbo].[WorkFlows] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Action]            NVARCHAR (MAX) NOT NULL,
    [FacilityTypeId]    INT            NOT NULL,
    [Rate]              NVARCHAR (MAX) NOT NULL,
    [TargetRole]        NVARCHAR (MAX) NOT NULL,
    [TriggeredByRole]   NVARCHAR (MAX) NOT NULL,
    [Status]            NVARCHAR (MAX) NOT NULL,
    [ApplicationTypeId] INT            NULL,
    [IsArchived]        BIT            CONSTRAINT [DF_WorkFlows_IsArchived] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WorkFlows] PRIMARY KEY CLUSTERED ([Id] ASC)
);









