Use BooksLibrary

CREATE TABLE Publishers
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Name NVARCHAR(Max) CHECK(Name != '') NOT NULL
)

CREATE TABLE Publishers
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Name NVARCHAR(Max) CHECK(Name != '') NOT NULL
)

CREATE TABLE Authors
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Firstname NVARCHAR(Max) CHECK(Firstname != '') NOT NULL,
	Surname NVARCHAR(Max) CHECK(Surname != '') NOT NULL,
	YearOfBirth INT NOT NULL
)

CREATE TABLE Books
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Title NVARCHAR(Max) CHECK(Title != '') NOT NULL,
	Pages INT CHECK(Pages > -1) NOT NULL,
	PublisherId INT NOT NULL REFERENCES Publishers(Id),
	AuthorID INT NOT NULL REFERENCES Authors(Id)
)

INSERT Publishers Values
(N'Издательство'),
(N'Печать')

INSERT Authors VALUES
(N'Клиффорд', N'Саймак', 1904),
(N'Джордж ', N'Оруэлл', 1903)

INSERT Books VALUES
(N'1984', 1350, (SELECT Id from Publishers WHERE Name = N'Издательство'), (SELECT Id from Authors WHERE Firstname = N'Джордж' AND  Surname = N'Оруэлл')),
(N'Город', 870, (SELECT Id from Publishers WHERE Name = N'Печать'), (SELECT Id from Authors WHERE Firstname = N'Клиффорд' AND  Surname = N'Саймак'))