Use Countries

TRUNCATE TABLE Countries

ALTER Table Countries DROP CONSTRAINT CK__Countries__Capit__4D94879B

ALTER Table Countries DROP COLUMN Capital

CREATE TABLE Capitals
(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Population INT CHECK (Population > 0) NOT NULL,
Name NVARCHAR(100) CHECK(Name != '') UNIQUE NOT NULL,
)

Create TABLE Towns
(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Population INT CHECK (Population > 0) NOT NULL,
Name NVARCHAR(100) CHECK(Name != '') UNIQUE NOT NULL,
)

Alter table Towns
ADD CountryID INT REFERENCES Countries(Id)

Alter TABLE Countries
ADD CapitalID INT REFERENCES Capitals(Id)

INSERT Capitals Values
(1275207, N'Антананариву'),
(812407, N'Банги'),
(4040, N'Сан-Марино')

Insert Countries Values
(N'Мадагаскар', 24823539, 17573900, N'Африка', (SELECT Id FROM Capitals WHERE Name = N'Антананариву')),
(N'ЦАР', 4892749, 622436, N'Африка', (SELECT Id FROM Capitals WHERE Name = N'Банги')),
(N'Сан-Марино', 33627, 61, N'Европа', (SELECT Id FROM Capitals WHERE Name = N'Сан-Марино'))

Insert Towns VALUES
(19320, N'Бука', (SELECT Id from Countries WHERE Name = N'ЦАР')),
(27071, N'Нделе', (SELECT Id from Countries WHERE Name = N'ЦАР')),
(14402, N'Обо', (SELECT Id from Countries WHERE Name = N'ЦАР')),
(101611, N'Тулиара', (SELECT Id from Countries WHERE Name = N'Мадагаскар')),
(16319, N'Амбилубе', (SELECT Id from Countries WHERE Name = N'Мадагаскар')),
(30000, N'Анталаха', (SELECT Id from Countries WHERE Name = N'Мадагаскар')),
(2137, N'Аккуавива', (SELECT Id from Countries WHERE Name = N'Сан-Марино')),
(6631, N'Борго-Маджоре', (SELECT Id from Countries WHERE Name = N'Сан-Марино')),
(2526, N'Фьорентино', (SELECT Id from Countries WHERE Name = N'Сан-Марино'))