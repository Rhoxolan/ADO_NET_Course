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

        private void button5_Click(object sender, EventArgs e)
        {
            //Метод написан для тестирования. Асинхронные методы (например ExecuteAsync) не работют из-за ограничений SQLite.
            //https://learn.microsoft.com/ru-ru/dotnet/standard/data/sqlite/async

            //Рекомендации с документации (Cache=Shared и WAL) не дали ощутимого эффекта, приложение "тормозит" на несколько секунд. Тем не менее, 500 000 записей
            //добавляются достаточно быстро - по ощущениям не более 5-и секунд.

            using SQLiteConnection sqliteConnection = new(connStr);
            StringBuilder queryStringBuilder = new();
            queryStringBuilder.AppendLine("""INSERT INTO Countries (Name) VALUES""");
            for (int i = 0; i < 500000; i++)
            {
                queryStringBuilder.AppendLine("""("Poland"),""");
            }
            queryStringBuilder.AppendLine("""("Poland")""");
            MessageBox.Show("Формирование запроса завершено!");
            try
            {
                var affectedRows = sqliteConnection.Execute(queryStringBuilder.ToString());
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

        private void button7_Click(object sender, EventArgs e)
        {
            using SQLiteConnection sqliteConnection = new(connStr);
            Buyer buyer = new Buyer
            {
                FullName = textBox2.Text,
                Gender = textBox3.Text,
                CityId = (int)numericUpDown2.Value
            };
            string query = """
                INSERT INTO Buyers(FullName, Gender, CityId) VALUES
                (@FullName, @Gender, @CityId)
                """;
            try
            {
                int affectedRows = sqliteConnection.Execute(query, buyer);
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

        private void button8_Click(object sender, EventArgs e)
        {
            using SQLiteConnection sqliteConnection = new(connStr);
            try
            {
                string querySelectBuyers = "SELECT * FROM Buyers";
                var buyers = sqliteConnection.Query<Buyer>(querySelectBuyers);
                Buyer buyer = buyers.Where(b => b.Id == (int)numericUpDown3.Value).First();
                buyer.FullName = textBox4.Text;
                string updateBuyer = "UPDATE Buyers SET FullName = @FullName WHERE Id = @Id";
                int affectedRows = sqliteConnection.Execute(updateBuyer, buyer);
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

        private void button9_Click(object sender, EventArgs e)
        {
            using SQLiteConnection sqliteConnection = new(connStr);
            string querySelectCountries = "SELECT * FROM Countries";
            var countries = sqliteConnection.Query(querySelectCountries); //Пример работы с dynamic
            dynamic country = countries.First(c => c.Id == (int)numericUpDown4.Value);
            country.FullName = textBox5.Text;

        }
    }
}