using System.Data.SqlClient;

namespace _2022._07._04_HW
{
    internal class Program
    {
        static void Main()
        {
            string connStr = "Data Source =(localdb)\\ MSSQLLocalDB; Initial Catalog=FruitsAndVegetables; Integrated Security=SSPI;";

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
                    //cmd.CommandText = "INSERT FruitsAndVegetables VALUES " +  //Добавляем строки в таблицу
                    //    "(N'Огурцы', 1, N'Зелёный', 15), " +
                    //    "(N'Абрикосы', 2, N'Оранжевый', 105), " +
                    //    "(N'Помидоры', 1, N'Красный', 55), " +
                    //    "(N'Чеснок', 1, N'Белый', 75), " +
                    //    "(N'Черемша', 1, N'Зелёный', 10)";
                    //Console.WriteLine($"{cmd.ExecuteNonQuery()} rows affected");

                    //Выбираем все значения
                    cmd.CommandText = "SELECT * FROM FruitsAndVegetables";
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    int line = 0;
                    while (sqlDataReader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            {
                                Console.Write($"{sqlDataReader.GetName(i),15}");
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine($"{sqlDataReader[0], 15}\t{sqlDataReader[1], 15}\t{sqlDataReader[2], 15}\t{sqlDataReader[3], 15}\t{sqlDataReader[4], 15}");
                        line++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("\n\n\n");
        }
    }
}