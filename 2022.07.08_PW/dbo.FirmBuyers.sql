﻿CREATE TABLE [dbo].[FirmBuyers] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([Name]<>'')
);

