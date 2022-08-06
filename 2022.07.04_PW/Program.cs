using System.Data.SqlClient;

namespace MyProgram
{
    class Program
    {
        static void Main()
        {
            string connStr = "Data Source =(localdb)\\ MSSQLLocalDB; Initial Catalog=StudentsGrades; Integrated Security=SSPI;";
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
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    conn?.Close();
                }
            }
        }
    }
}