USE Games

--Показать топ-3 студий с максимальным количеством игр;

CREATE VIEW ShowTop3PublishersPerGames AS
SELECT TOP(3) Publishers.Name AS Publisher, COUNT(Games.Id) AS N'Games Count'
FROM Publishers, Games
WHERE Games.PublisherId = Publishers.Id
GROUP BY Publishers.Name
ORDER BY COUNT(Games.Id) DESC