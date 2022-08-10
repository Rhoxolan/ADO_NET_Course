CREATE TABLE [dbo].[Products] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [TypeId]         INT           NOT NULL,
    [ProviderId]     INT           NOT NULL,
    [Name]           NVARCHAR (30) NOT NULL,
    [Amount]         INT           DEFAULT ((0)) NOT NULL,
    [CostPrice]      MONEY         NOT NULL,
    [DateOfDelivery] DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TypeId]) REFERENCES [dbo].[Types] ([Id]),
    FOREIGN KEY ([ProviderId]) REFERENCES [dbo].[ProductProvider] ([Id]),
    CHECK ([Name]<>''),
    CHECK ([CostPrice]>(1)),
    CHECK ([Amount] > (-1))
);

