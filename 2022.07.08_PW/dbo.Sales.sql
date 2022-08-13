CREATE TABLE [dbo].[Sales] (
    [Id]           INT   IDENTITY (1, 1) NOT NULL,
    [StationeryId] INT   NOT NULL,
    [SalesmanId]   INT   NOT NULL,
    [FirmBuyerId]  INT   NOT NULL,
    [DateOfSale]   DATE  NOT NULL,
    [UnitPrice]    MONEY NOT NULL,
    [UnitsAmount]  INT   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([StationeryId]) REFERENCES [dbo].[Stationeries] ([Id]),
    FOREIGN KEY ([SalesmanId]) REFERENCES [dbo].[Salesmans] ([Id]),
    FOREIGN KEY ([FirmBuyerId]) REFERENCES [dbo].[FirmBuyers] ([Id]),
    CHECK ([UnitPrice]>(-1)),
    CHECK ([UnitsAmount]>(-1))
);

