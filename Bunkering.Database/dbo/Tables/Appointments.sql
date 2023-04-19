CREATE TABLE [dbo].[Appointments] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationId]   INT            NOT NULL,
    [AppointmentDate] DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [ApprovalMessage] NVARCHAR (MAX) NULL,
    [ApprovedBy]      NVARCHAR (MAX) NULL,
    [ClientMessage]   NVARCHAR (MAX) NULL,
    [ContactName]     NVARCHAR (MAX) NULL,
    [ExpiryDate]      DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [IsAccepted]      BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsApproved]      BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [PhoneNo]         NVARCHAR (MAX) NULL,
    [ScheduleDate]    DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [ScheduleMessage] NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [ScheduledBy]     NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [ScheduleType]    NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Appointments_Applications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Applications] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Appointments_ApplicationId]
    ON [dbo].[Appointments]([ApplicationId] ASC);

