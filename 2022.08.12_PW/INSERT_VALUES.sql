INSERT INTO Countries (Name) VALUES
("Poland"),
("Ukraine")

INSERT INTO Cities (Name, CountryId) VALUES
("Warszawa", (SELECT Id FROM Countries WHERE Name = "Poland")),
("Тернопіль", (SELECT Id FROM Countries WHERE Name = "Ukraine"))

INSERT INTO Buyers (FullName, Gender, CityId) VALUES
("Marek Mirecki", "Male", (SELECT Id FROM Cities WHERE Name = "Warszawa")),
("Оксана Лагута", "Female", (SELECT Id FROM Cities WHERE Name = "Тернопіль"))

INSERT INTO MailingListsMobilePhones (BuyerId) VALUES
((SELECT Id FROM Buyers WHERE FullName = "Marek Mirecki"))

INSERT INTO MailingListsLaptops (BuyerId) VALUES
((SELECT Id FROM Buyers WHERE FullName = "Оксана Лагута"))