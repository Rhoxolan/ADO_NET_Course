CREATE TABLE [dbo].[FruitsAndVegetables] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (100) NOT NULL,
    [Type]     INT            NOT NULL,
    [Color]    NVARCHAR (30)  NOT NULL,
    [Calories] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([Name]<>''),
    CHECK ([Color]<>'')
);
