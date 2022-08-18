CREATE TABLE "FruitsAndVegetables" (
	"Id"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL CHECK(Name<>''),
	"Type"	INTEGER NOT NULL,
	"Color"	TEXT NOT NULL CHECK(Color<>''),
	"Calories"	INTEGER,
	PRIMARY KEY("Id")
)