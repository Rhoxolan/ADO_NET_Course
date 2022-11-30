USE Games

--Показать топ-3 студий с максимальным количеством игр;
CREATE VIEW ShowTop3PublishersPerGames AS
SELECT TOP(3) Publishers.Name AS Publisher, COUNT(Games.Id) AS N'GamesCount'
FROM Publishers, Games
WHERE Games.PublisherId = Publishers.Id
GROUP BY Publishers.Name
ORDER BY COUNT(Games.Id) DESC


--Показать студию с максимальным количеством игр;CREATE VIEW ShowTopPublisherPerGames AS
SELECT TOP(1) Publishers.Name AS Publisher, COUNT(Games.Id) AS N'GamesCount'
FROM Publishers, Games
WHERE Games.PublisherId = Publishers.Id
GROUP BY Publishers.Name
ORDER BY COUNT(Games.Id) DESC


--Показать топ-3 самых популярных стилей по количеству игр;
CREATE VIEW ShowTopGenresPerGames AS
SELECT TOP(3) Genres.Name As Genre, COUNT(Games.Id) AS GamesCount
FROM Genres, Games
WHERE Games.GenreId = Genres.Id
GROUP BY Genres.Name
ORDER BY COUNT(Games.Id) DESC


CREATE TABLE Sales
(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Date DATE NOT NULL,
GameId INT NOT NULL REFERENCES Games(Id)
)


--Показать топ-3 самых популярных стилей по количеству продаж;
CREATE VIEW ShowTop3StylesPerSales AS
SELECT TOP(3) Genres.Name As Style, COUNT(Sales.Id) As Sales
FROM Genres, Games, Sales
WHERE Genres.Id = Games.GenreId AND Sales.GameId = Games.Id
GROUP BY Genres.Name
ORDER BY COUNT(Sales.Id) DESC