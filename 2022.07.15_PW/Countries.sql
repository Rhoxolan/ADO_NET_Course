USE Countries

CREATE TABLE Countries
(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Name NVARCHAR(max) CHECK(Name != '') NOT NULL,
Capital NVARCHAR(Max) CHECK(Capital != '') NOT NULL,
Population INT CHECK(Population > 0) NOT NULL,
Area INT CHECK(Area > 0) NOT NULL,
PartOfTheWorld NVARCHAR(Max) CHECK(PartOfTheWorld != '') NOT NULL,
)

INSERT Countries Values
(N'Буркина-Фасо', N'Уагадугу', 21000000, 273187, N'Африка'),
(N'Мадагаскар', N'Антананариву', 24823539, 587041, N'Африка'),
(N'Лихтенштейн', N'Вадуц', 38829, 160, N'Европа'),
(N'Андорра', N'Андорра-ла-Велья', 82887, 468, N'Европа'),
(N'Гренландия', N'Нуук', 56421, 2175600, N'Северная Америка')