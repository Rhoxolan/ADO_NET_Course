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
                    Console.WriteLine($"{sqlDataReader.GetName(0)}\t{sqlDataReader.GetName(1)}\t{sqlDataReader.GetName(2)}\t{sqlDataReader.GetName(3)}\t{sqlDataReader.GetName(4)}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}\t{sqlDataReader.GetValue(1)}\t{sqlDataReader.GetValue(2)}\t{sqlDataReader.GetValue(3)}\t{sqlDataReader.GetValue(4)}");
                    }
                    //Ты тут. Разобраться с красивым выводом.
                    sqlDataReader.Close();
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