CREATE TABLE [dbo].[Applications] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [UserId]            NVARCHAR (450) NOT NULL,
    [FacilityId]        INT            NOT NULL,
    [ApplicationTypeId] INT            DEFAULT ((0)) NOT NULL,
    [CreatedDate]       DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [CurrentDeskId]     NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [ModifiedDate]      DATETIME2 (7)  NULL,
    [Reference]         NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [FlowId]            INT            NULL,
    [SubmittedDate]     DATETIME2 (7)  NULL,
    [Status]            NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [FADApproved]       BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [FADStaffId]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Applications_ApplicationTypes_ApplicationTypeId] FOREIGN KEY ([ApplicationTypeId]) REFERENCES [dbo].[ApplicationTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Applications_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Applications_Facilities_FacilityId] FOREIGN KEY ([FacilityId]) REFERENCES [dbo].[Facilities] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Applications_WorkFlows_FlowId] FOREIGN KEY ([FlowId]) REFERENCES [dbo].[WorkFlows] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Applications_FacilityId]
    ON [dbo].[Applications]([FacilityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Applications_UserId]
    ON [dbo].[Applications]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Applications_ApplicationTypeId]
    ON [dbo].[Applications]([ApplicationTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Applications_FlowId]
    ON [dbo].[Applications]([FlowId] ASC);

