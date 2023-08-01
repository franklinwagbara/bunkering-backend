CREATE TABLE [dbo].[Applications] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationTypeId] INT            NOT NULL,
    [UserId]            NVARCHAR (450) NOT NULL,
    [FacilityId]        INT            NOT NULL,
    [Reference]         NVARCHAR (MAX) NOT NULL,
    [CurrentDeskId]     NVARCHAR (MAX) NOT NULL,
    [FADStaffId]        NVARCHAR (MAX) NULL,
    [FADApproved]       BIT            NOT NULL,
    [CreatedDate]       DATETIME2 (7)  NOT NULL,
    [SubmittedDate]     DATETIME2 (7)  NULL,
    [ModifiedDate]      DATETIME2 (7)  NULL,
    [Status]            NVARCHAR (MAX) NOT NULL,
    [FlowId]            INT            NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_Applications_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([Id] ASC)
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

