CREATE TABLE [dbo].[WorkFlows] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Action]          NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [FacilityTypeId]  INT            DEFAULT ((0)) NOT NULL,
    [Rate]            NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [TargetRole]      NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [TriggeredByRole] NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [Status]          NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    CONSTRAINT [PK_WorkFlows] PRIMARY KEY CLUSTERED ([Id] ASC)
);

