using Npgsql;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace _2022._07._13_HW
{
    public partial class Form1 : Form
    {
        string connStr;
        DataTable dt;
        DbProviderFactory providerFactory;

        public Form1()
        {
            InitializeComponent();
            connStr = string.Empty;
            dt = null;
            providerFactory = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            //Подгрузка двух вариантов подключений к базам данных
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
            DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);
            dt = DbProviderFactories.GetFactoryClasses();
            comboBox1.DataSource = null;
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "InvariantName";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Присваиваем переменной providerFactory значение выбранной базы данных 
            if (comboBox1.SelectedIndex != -1)
            {
                DataRowView selectedItem = comboBox1.SelectedItem as DataRowView;
                string providerName = selectedItem.Row.Field<string>("InvariantName");
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings.OfType<ConnectionStringSettings>().LastOrDefault(t => t.ProviderName == providerName);
                if (settings != null)
                {
                    //MessageBox.Show(settings.ConnectionString);
                    connStr = settings.ConnectionString;
                    providerFactory = DbProviderFactories.GetFactory(providerName);
                }
            }
        }

        //Универсальный метод для выполнения запросов
        private DataTable ExecuteQuery(string query)
        {
            //MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            //Thread.Sleep(5000);
            using (DbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = connStr;
                DbCommand command = providerFactory.CreateCommand();
                command.CommandText = query;
                command.Connection = connection;
                DataTable dataTable = new();
                DbDataReader reader = null;
                try
                {
                    connection?.Open();
                    reader = command.ExecuteReader();
                    int line = 0;
                    dataTable.Columns.Clear();
                    while (reader.Read())
                    {
                        if (line == 0)
                            for (int i = 0; i < reader.FieldCount; i++)
                                dataTable.Columns.Add(reader.GetName(i));
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                            row[i] = reader[i];
                        dataTable.Rows.Add(row);
                        line++;
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                finally
                {
                    reader?.Close();
                    connection?.Close();
                }
            }
        }

        //Универсальный метод для выполнения запросов, вариант с передачей объекта DbCommand для выполнения параметризованного запроса.
        private DataTable ExecuteQuery2(DbCommand command)
        {
            //MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            using (DbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = connStr;
                command.Connection = connection;
                DataTable dataTable = new();
                DbDataReader reader = null;
                try
                {
                    connection?.Open();
                    reader = command.ExecuteReader();
                    int line = 0;
                    dataTable.Columns.Clear();
                    while (reader.Read())
                    {
                        if (line == 0)
                            for (int i = 0; i < reader.FieldCount; i++)
                                dataTable.Columns.Add(reader.GetName(i));
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                            row[i] = reader[i];
                        dataTable.Rows.Add(row);
                        line++;
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                finally
                {
                    reader?.Close();
                    connection?.Close();
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await Task.Run(() => ExecuteQuery("SELECT * FROM FruitsAndVegetables")); //Запускаем выполнение асинхронного метода
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = comboBox1.SelectedItem as DataRowView;
            if (selectedItem.Row.Field<string>("InvariantName") == "Npgsql") //Эксперементируем с передачей команды универсальному методу для выполнения запроса
            {
                string query = $"INSERT INTO FruitsAndVegetables (Name, Type, Color, Calories) VALUES (@Name, @Type, @Color, @Calories)";
                DbCommand dbCommand = providerFactory.CreateCommand();
                NpgsqlCommand command = (NpgsqlCommand)dbCommand;
                command.Parameters.AddWithValue("@Name", textBox1.Text);
                command.Parameters.AddWithValue("@Type", Convert.ToInt32(textBox2.Text));
                command.Parameters.AddWithValue("@Color", textBox3.Text);
                command.Parameters.AddWithValue("@Calories", Convert.ToInt32(textBox4.Text));
                command.CommandText = query;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = await Task.Run(() => ExecuteQuery2(command));   //Запускаем выполнение асинхронного метода
            }
            if (selectedItem.Row.Field<string>("InvariantName") == "System.Data.SqlClient")
            {
                string query = $"INSERT INTO FruitsAndVegetables (Name, Type, Color, Calories) VALUES (@Name, @Type, @Color, @Calories)";
                DbCommand dbCommand = providerFactory.CreateCommand();
                SqlCommand command = (SqlCommand)dbCommand;
                command.Parameters.AddWithValue("@Name", textBox1.Text);
                command.Parameters.AddWithValue("@Type", Convert.ToInt32(textBox2.Text));
                command.Parameters.AddWithValue("@Color", textBox3.Text);
                command.Parameters.AddWithValue("@Calories", Convert.ToInt32(textBox4.Text));
                command.CommandText = query;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = await Task.Run(() => ExecuteQuery2(command));   //Запускаем выполнение асинхронного метода
            }
            if (selectedItem.Row.Field<string>("InvariantName") == "System.Data.SQLite")
            {
                string query = $"INSERT INTO FruitsAndVegetables (Name, Type, Color, Calories) VALUES (@Name, @Type, @Color, @Calories)";
                DbCommand dbCommand = providerFactory.CreateCommand();
                SQLiteCommand command = (SQLiteCommand)dbCommand;
                command.Parameters.AddWithValue("@Name", textBox1.Text);
                command.Parameters.AddWithValue("@Type", Convert.ToInt32(textBox2.Text));
                command.Parameters.AddWithValue("@Color", textBox3.Text);
                command.Parameters.AddWithValue("@Calories", Convert.ToInt32(textBox4.Text));
                command.CommandText = query;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = await Task.Run(() => ExecuteQuery2(command));   //Запускаем выполнение асинхронного метода
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await Task.Run(() => ExecuteQuery($"DELETE FROM FruitsAndVegetables WHERE Id = {id}"));  //Запускаем выполнение асинхронного метода
        }
    }
}