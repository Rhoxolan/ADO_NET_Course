using Npgsql;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

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
            //Подгрузка двух вариантов подключений к базам данных
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
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
        private async void ExecuteQuery(string query)
        {
            using (DbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = connStr;
                DbCommand command = providerFactory.CreateCommand();
                command.CommandText = query;
                command.Connection = connection;
                DataTable dt2 = new();
                DbDataReader reader = null;
                try
                {
                    await connection?.OpenAsync();
                    reader = await command.ExecuteReaderAsync();
                    int line = 0;
                    dt2.Columns.Clear();
                    while (await reader.ReadAsync())
                    {
                        if (line == 0)
                            for (int i = 0; i < reader.FieldCount; i++)
                                dt2.Columns.Add(reader.GetName(i));
                        DataRow row = dt2.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                            row[i] = reader[i];
                        dt2.Rows.Add(row);
                        line++;
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt2;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    reader?.Close();
                    connection?.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecuteQuery("SELECT * FROM FruitsAndVegetables");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = comboBox1.SelectedItem as DataRowView;
            if (selectedItem.Row.Field<string>("InvariantName") == "Npgsql")
            {
                ExecuteQuery($"INSERT INTO FruitsAndVegetables VALUES (DEFAULT, '{textBox1.Text}', {Convert.ToInt32(textBox2.Text)}, '{textBox3.Text}', {Convert.ToInt32(textBox4.Text)})");
            }
            if (selectedItem.Row.Field<string>("InvariantName") == "System.Data.SqlClient")
            {
                ExecuteQuery($"INSERT INTO FruitsAndVegetables VALUES (N'{textBox1.Text}', {Convert.ToInt32(textBox2.Text)}, N'{textBox3.Text}', {Convert.ToInt32(textBox4.Text)})");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            ExecuteQuery($"DELETE FROM FruitsAndVegetables WHERE Id = {id}");
        }
    }
}