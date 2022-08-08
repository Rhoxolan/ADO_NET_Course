using System.Data.SqlClient;

namespace MyProgram
{
    class Program
    {
        static void Main()
        {
            string connStr = "Data Source =(localdb)\\ MSSQLLocalDB; Initial Catalog=StudentsGrades; Integrated Security=SSPI;";

            //Задание 2
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine($"Server Name: {conn.DataSource}");
                    Console.WriteLine($"DataBase Name: {conn.Database}");
                    Console.WriteLine($"ServerVersion: {conn.ServerVersion}");
                    Console.WriteLine($"Connection State: {conn.State}");
                    Console.WriteLine("\nПодключение было выполнено успешно!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conn?.Close();
                }
            }
            Console.WriteLine("\n\n\n");


            //Задание 3
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new();
                    cmd.Connection = conn;
                    //cmd.CommandText = "INSERT StudentsGrades VALUES " +  //Добавляем строки в таблицу
                    //    "(N'Петров Петр Петрович', N'Группа 5', 10, N'История', N'География'), " +
                    //    "(N'Максименко Максим Максимович', N'Группа 7', 9, N'Английский', N'Математика'), " +
                    //    "(N'Алексеенко Алексей Алексеевич', N'Группа 3', 5, N'История', N'Математика')";
                    //cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT * FROM StudentsGrades"; //Выбираем все значения
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}\t{sqlDataReader.GetName(1),30}\t{sqlDataReader.GetName(2),30}\t{sqlDataReader.GetName(3),30}\t{sqlDataReader.GetName(4),30}\t{sqlDataReader.GetName(5),30}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}\t{sqlDataReader.GetValue(1),30}\t{sqlDataReader.GetValue(2),30}\t{sqlDataReader.GetValue(3),30}\t{sqlDataReader.GetValue(4),30}\t{sqlDataReader.GetValue(5),30}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();


                    cmd.CommandText = "SELECT FullName AS ФИО FROM StudentsGrades"; //Выбираем ФИО
                    sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();

                    cmd.CommandText = "SELECT AverageGradesForYears AS 'Средняя оценка' FROM StudentsGrades"; //Выбираем ФИО
                    sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();

                    cmd.CommandText = "SELECT AverageGradesForYears AS 'Средняя оценка', FullName AS ФИО FROM StudentsGrades " +
                        "WHERE AverageGradesForYears > 9"; //Показать ФИО всех студентов с минимальной оценкой, больше, чем указанная;
                    sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}{sqlDataReader.GetName(1), 30}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}{sqlDataReader.GetValue(1), 30}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();

                    //Показать название всех предметов с минимальными средними оценками. Названия предметов должны быть уникальными.
                    cmd.CommandText = "SELECT DISTINCT NameOfMinAVGGradesSubject FROM StudentsGrades";
                    sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conn?.Close();
                }
            }
            Console.WriteLine("\n\n\n");

            //Задание 4.
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new();
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT MIN(AverageGradesForYears) FROM StudentsGrades"; //Показать минимальную среднюю оценку;
                    object minGrades = cmd.ExecuteScalar();
                    Console.WriteLine($"Минимальная средняя оценка: {minGrades}");
                    Console.WriteLine("\n");

                    cmd.CommandText = "SELECT MAX(AverageGradesForYears) FROM StudentsGrades"; //Показать максимальную среднюю оценку;
                    object maxGrades = cmd.ExecuteScalar();
                    Console.WriteLine($"Максимальная средняя оценка: {maxGrades}");
                    Console.WriteLine("\n");

                    //Показать количество студентов, у которых минимальная средняя оценка по математике;
                    cmd.CommandText = "SELECT COUNT(StudentsGrades.Id) FROM StudentsGrades WHERE StudentsGrades.NameOfMinAVGGradesSubject = N'Математика'";
                    object mathematicsLowStudents = cmd.ExecuteScalar();
                    Console.WriteLine($"Количество студентов, у которых минимальная средняя оценка по математике: {mathematicsLowStudents}");
                    Console.WriteLine("\n");

                    //Показать количество студентов, у которых маскимальная средняя оценка по математике;
                    cmd.CommandText = "SELECT COUNT(StudentsGrades.Id) FROM StudentsGrades WHERE StudentsGrades.NameOfMaxAVGGradesSubject = N'Математика'";
                    object mathematicsHightStudents = cmd.ExecuteScalar();
                    Console.WriteLine($"Количество студентов, у которых маскимальная средняя оценка по математике: {mathematicsHightStudents}");
                    Console.WriteLine("\n");

                    //Показать количество студентов в каждой группе;
                    cmd.CommandText = "SELECT COUNT(StudentsGrades.Id) FROM StudentsGrades WHERE StudentsGrades.GroupName = N'Группа 5'";
                    object gr5studentsCount = cmd.ExecuteScalar();
                    cmd.CommandText = "SELECT COUNT(StudentsGrades.Id) FROM StudentsGrades WHERE StudentsGrades.GroupName = N'Группа 7'";
                    object gr7studentsCount = cmd.ExecuteScalar();
                    cmd.CommandText = "SELECT COUNT(StudentsGrades.Id) FROM StudentsGrades WHERE StudentsGrades.GroupName = N'Группа 3'";
                    object gr3studentsCount = cmd.ExecuteScalar();
                    Console.WriteLine($"Группа 5 - {gr5studentsCount} чел., Группа 7 - {gr7studentsCount} чел., Группа 3 - {gr3studentsCount} чел.");
                    Console.WriteLine("\n");

                    //Показать среднюю оценку по группе.
                    cmd.CommandText = "SELECT AVG(StudentsGrades.AverageGradesForYears) FROM StudentsGrades WHERE StudentsGrades.GroupName = N'Группа 5'";
                    object avgGroup5grade = cmd.ExecuteScalar();
                    cmd.CommandText = "SELECT AVG(StudentsGrades.AverageGradesForYears) FROM StudentsGrades WHERE StudentsGrades.GroupName = N'Группа 7'";
                    object avgGroup7grade = cmd.ExecuteScalar();
                    cmd.CommandText = "SELECT AVG(StudentsGrades.AverageGradesForYears) FROM StudentsGrades WHERE StudentsGrades.GroupName = N'Группа 3'";
                    object avgGroup3grade = cmd.ExecuteScalar();
                    Console.WriteLine($"Средняя оценка Гр.5 - {avgGroup5grade}, Средняя оценка Гр.7 - {avgGroup7grade}, Средняя оценка Гр.3 - {avgGroup3grade},");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conn?.Close();
                }
            }
            Console.WriteLine("\n\n\n");
        }
    }
}