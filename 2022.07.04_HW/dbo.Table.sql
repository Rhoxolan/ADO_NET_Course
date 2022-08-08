CREATE TABLE [dbo].[FruitsAndVegetables]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL CHECK(Name != ''),
	[Type] BIT NOT NULL,
	[Color] NVARCHAR(30) NOT NULL CHECK(Color != ''),
	[Calories] INT NULL
)
