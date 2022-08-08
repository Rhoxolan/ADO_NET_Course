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
                    Console.WriteLine("\n");
                    sqlDataReader.Close();

                    //Выбираем названия фруктов и овощей
                    cmd.CommandText = "SELECT Name FROM FruitsAndVegetables";
                    sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();

                    //Отображение всех цветов;
                    cmd.CommandText = "SELECT Color FROM FruitsAndVegetables";
                    sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();

                    //Выполнение нескольких запросов сразу через NextResult()
                    //Показать максимальную, минимальную и среднюю калорийность;
                    cmd.CommandText = "SELECT DISTINCT Calories FROM FruitsAndVegetables WHERE Calories = (SELECT MAX(Calories) FROM FruitsAndVegetables); " +
                        "SELECT DISTINCT Calories FROM FruitsAndVegetables WHERE Calories = (SELECT MIN(Calories) FROM FruitsAndVegetables); " +
                        "SELECT DISTINCT Calories FROM FruitsAndVegetables WHERE Calories BETWEEN ((SELECT AVG(Calories) FROM FruitsAndVegetables) - 30) AND ((SELECT AVG(Calories) FROM FruitsAndVegetables) + 30);";
                    sqlDataReader = cmd.ExecuteReader();
                    int line2 = 0;
                    do
                    {
                        while (sqlDataReader.Read())
                        {
                            if (line2 == 0)
                            {
                                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                                {
                                    Console.Write($"{sqlDataReader.GetName(i),15}");
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine($"{sqlDataReader[0],15}");
                            line2++;
                        }
                    } while (sqlDataReader.NextResult());
                    Console.WriteLine("\n");
                    sqlDataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("\n\n\n");

            //Задание 4
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new();
                    cmd.Connection = conn;

                    //Показать количество овощей;
                    cmd.CommandText = "SELECT COUNT(FruitsAndVegetables.Id) FROM FruitsAndVegetables WHERE FruitsAndVegetables.Type = 2";
                    object countVeg = cmd.ExecuteScalar();
                    Console.WriteLine($"Количество овощей: {countVeg}");
                    Console.WriteLine("\n");

                    //Показать количество фруктов
                    cmd.CommandText = "SELECT COUNT(FruitsAndVegetables.Id) FROM FruitsAndVegetables WHERE FruitsAndVegetables.Type = 1";
                    object countFruit = cmd.ExecuteScalar();
                    Console.WriteLine($"Количество овощей: {countFruit}");
                    Console.WriteLine("\n");

                    //Показать количество овощей и фруктов заданного цвета;
                    cmd.CommandText = "SELECT COUNT(FruitsAndVegetables.Id) FROM FruitsAndVegetables WHERE FruitsAndVegetables.Color = N'Зелёный'";
                    object countFruitNVegAtColor = cmd.ExecuteScalar();
                    Console.WriteLine($"Количество овощей и фруктов заданного цвета: {countFruitNVegAtColor}");
                    Console.WriteLine("\n");

                    //Показать количество овощей фруктов каждого цвета;
                    cmd.CommandText = "SELECT COUNT(Color) FROM FruitsAndVegetables";
                    object countAllColors = cmd.ExecuteScalar();
                    Console.WriteLine($"Количество овощей и фруктов каждого цвета: {countAllColors}");
                    Console.WriteLine("\n");

                    //Показать овощи и фрукты с калорийностью ниже указанной;
                    cmd.CommandText = "SELECT Name, Calories FROM FruitsAndVegetables " +
                        "WHERE Calories < 20";
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}{sqlDataReader.GetName(1),30}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}{sqlDataReader.GetValue(1),30}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();

                    //Показать овощи и фрукты с калорийностью выше указанной;
                    cmd.CommandText = "SELECT Name, Calories FROM FruitsAndVegetables " +
                        "WHERE Calories > 20";
                    sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}{sqlDataReader.GetName(1),30}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}{sqlDataReader.GetValue(1),30}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();

                    //Показать овощи и фрукты с калорийностью в указанном диапазоне;
                    cmd.CommandText = "SELECT Name, Calories FROM FruitsAndVegetables " +
                        "WHERE Calories BETWEEN 10 AND 20";
                    sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}{sqlDataReader.GetName(1),30}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}{sqlDataReader.GetValue(1),30}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();

                    //Показать все овощи и фрукты, у которых цвет желтый или красный.
                    cmd.CommandText = "SELECT Name, Color FROM FruitsAndVegetables " +
                        "WHERE Color = N'Зелёный' OR Color = N'Красный'";
                    sqlDataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{sqlDataReader.GetName(0)}{sqlDataReader.GetName(1),30}");
                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine($"{sqlDataReader.GetValue(0)}{sqlDataReader.GetValue(1),30}");
                    }
                    Console.WriteLine("\n");
                    sqlDataReader.Close();
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