CREATE TABLE [dbo].[WorkFlows] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FacilityTypeId]  INT            NOT NULL,
    [TriggeredByRole] NVARCHAR (MAX) NOT NULL,
    [Action]          NVARCHAR (MAX) NOT NULL,
    [TargetRole]      NVARCHAR (MAX) NOT NULL,
    [Rate]            NVARCHAR (MAX) NOT NULL,
    [Status]          NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_WorkFlows] PRIMARY KEY CLUSTERED ([Id] ASC)
);



