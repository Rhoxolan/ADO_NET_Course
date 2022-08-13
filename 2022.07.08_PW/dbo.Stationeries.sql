CREATE TABLE [dbo].[Stationeries] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    [TypeId] INT            NOT NULL,
    [Price]  MONEY          NOT NULL,
    [Amount] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TypeId]) REFERENCES [dbo].[StationeryTypes] ([Id]),
    CHECK ([Name]<>''),
    CHECK ([Price]>(-1)),
    CHECK ([Amount]>(-1))
);

