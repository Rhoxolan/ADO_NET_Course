CREATE TABLE [dbo].[StudentsGrades]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FullName] NVARCHAR(100) NOT NULL CHECK(FullName != ''), 
    [GroupName] NVARCHAR(30) NOT NULL DEFAULT 'Приёмная группа', 
    [AverageGradesForYears] FLOAT NULL, 
    [NameOfMinAVGGradesSubject] NVARCHAR(30) NULL, 
    [NameOfMaxAVGGradesSubject] NVARCHAR(30) NULL,
)