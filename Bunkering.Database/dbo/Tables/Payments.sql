﻿CREATE TABLE [dbo].[Payments] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [ApplicationId]      INT             NOT NULL,
    [ExtraPaymentId]     INT             NULL,
    [PaymentType]        NVARCHAR (MAX)  NOT NULL,
    [TransactionDate]    DATETIME2 (7)   NOT NULL,
    [TransactionId]      NVARCHAR (MAX)  NOT NULL,
    [RRR]                NVARCHAR (MAX)  NOT NULL,
    [Description]        NVARCHAR (MAX)  NOT NULL,
    [AppReceiptId]       NVARCHAR (MAX)  NOT NULL,
    [Amount]             DECIMAL (18, 2) NOT NULL,
    [Arrears]            DECIMAL (18, 2) NOT NULL,
    [ServiceCharge]      DECIMAL (18, 2) NOT NULL,
    [TxnMessage]         NVARCHAR (MAX)  NOT NULL,
    [RetryCount]         INT             NULL,
    [LastRetryDate]      DATETIME2 (7)   NULL,
    [Account]            NVARCHAR (MAX)  NOT NULL,
    [BankCode]           NVARCHAR (MAX)  NOT NULL,
    [LateRenewalPenalty] DECIMAL (18, 2) NOT NULL,
    [NonRenewalPenalty]  DECIMAL (18, 2) NOT NULL,
    [Status]             NVARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO
CREATE NONCLUSTERED INDEX [IX_Payments_ApplicationId]
    ON [dbo].[Payments]([ApplicationId] ASC);


GO


