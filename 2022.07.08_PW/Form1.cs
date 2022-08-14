using System.Configuration;
using System.Data.SqlClient;

namespace _2022._07._08_PW
{
    public partial class Form1 : Form
    {
        string connStr;

        public Form1()
        {
            connStr = ConfigurationManager.ConnectionStrings["mySqlConn"].ConnectionString;
            InitializeComponent();
        }

        //Задание 2. Проверка подключения
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Подключение было выполнено успешно!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    conn?.Close();
                }
                finally
                {
                    conn?.Close();
                }
            }
        }


    }
}