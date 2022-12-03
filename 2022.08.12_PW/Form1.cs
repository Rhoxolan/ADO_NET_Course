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
    }
}