CREATE TABLE [dbo].[AppFees] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [ApplicationTypeId] INT             NOT NULL,
    [FacilityTypeId]    INT             NOT NULL,
    [ApplicationFee]    DECIMAL (18, 2) NOT NULL,
    [AccreditationFee]  DECIMAL (18, 2) NOT NULL,
    [VesselLicenseFee]  DECIMAL (18, 2) NOT NULL,
    [AdministrativeFee] DECIMAL (18, 2) NOT NULL,
    [InspectionFee]     DECIMAL (18, 2) NOT NULL,
    [SerciveCharge]     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AppFees] PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO
CREATE NONCLUSTERED INDEX [IX_AppFees_ApplicationTypeId]
    ON [dbo].[AppFees]([ApplicationTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AppFees_FacilityTypeId]
    ON [dbo].[AppFees]([FacilityTypeId] ASC);

