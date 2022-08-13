CREATE TABLE [dbo].[Salesmans] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [Surname] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([Name]<>''),
    CHECK ([Surname]<>'')
);

