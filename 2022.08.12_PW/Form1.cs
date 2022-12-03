using Dapper;
using System.Collections;
using System.Data.SQLite;

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
            try
            {
                string query = "SELECT * FROM Cities";
                dataGridView1.DataSource = sqliteConnection.Query<City>(query).ToList();
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

        public class City
        {
            public required int Id { get; set; }

            public required string Name { get; set; }

            public required int CountryId { get; set; }
        }
    }
}