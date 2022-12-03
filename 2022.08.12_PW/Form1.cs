using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace _2022._08._12_PW
{
    public partial class Form1 : Form
    {
        private string connStr;

        public Form1()
        {
            InitializeComponent();
            connStr = "Data Source=MailingListsPromotions.db;Cache=Shared";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using SQLiteConnection sqliteConnection = new(connStr);
            try
            {
                sqliteConnection.Open();
                MessageBox.Show("Подключение было выполнено успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqliteConnection?.Close();
            }
            finally
            {
                sqliteConnection?.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using SQLiteConnection sqliteConnection = new(connStr);
            try
            {
                string query = "SELECT * FROM Buyers";
                dataGridView1.DataSource = sqliteConnection.Query<Buyer>(query).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqliteConnection?.Close();
            }
            finally
            {
                sqliteConnection?.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using SQLiteConnection sqliteConnection = new(connStr);
            var parameters = new { paramId = numericUpDown1.Value };
            string query = "SELECT * FROM Cities WHERE Id = @paramId"; //Работа с параметрами
            try
            {
                var city = sqliteConnection.QuerySingle(query, parameters);
                dataGridView1.DataSource = new ArrayList { new { city.Name } }; //Работа с типом dynamic
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqliteConnection?.Close();
            }
            finally
            {
                sqliteConnection?.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using SQLiteConnection sqliteConnection = new(connStr);
            var parameters = new { paramId = textBox1.Text };
            string query = "SELECT * FROM Buyers WHERE CityId = (SELECT Id FROM Cities WHERE Name = @paramId)";
            try
            {
                dataGridView1.DataSource = sqliteConnection.Query<Buyer>(query, parameters).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqliteConnection?.Close();
            }
            finally
            {
                sqliteConnection?.Close();
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            using SQLiteConnection sqliteConnection = new(connStr);
            StringBuilder queryStringBuilder = new();
            queryStringBuilder.AppendLine("""INSERT INTO Countries (Name) VALUES ("USA")""");
            //for (int i = 0; i < 10; i++)
            //{
            //    queryStringBuilder.AppendLine("""("Poland"),""");
            //}
            MessageBox.Show("Формирование запроса завершено!");
            try
            {
                var affectedRows = await sqliteConnection.ExecuteAsync(queryStringBuilder.ToString());
                MessageBox.Show($"Affected Rows: {affectedRows}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqliteConnection?.Close();
            }
            finally
            {
                sqliteConnection?.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using SQLiteConnection sqliteConnection = new(connStr);
            try
            {
                string query = "SELECT * FROM Countries";
                dataGridView1.DataSource = sqliteConnection.Query<Country>(query).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqliteConnection?.Close();
            }
            finally
            {
                sqliteConnection?.Close();
            }
        }
    }
}