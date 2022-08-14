using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _2022._07._08_PW
{
    public partial class Form1 : Form
    {
        string connStr;
        DataTable dt;
        List<StationeryType> types;

        public Form1()
        {
            InitializeComponent();
            connStr = ConfigurationManager.ConnectionStrings["mySqlConn"].ConnectionString;
            types = new();
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

        //Задание 3.1. Отображение всей информации о канцтоварах
        private void button2_Click(object sender, EventArgs e)
        {
            ShowFromDb("SELECT * FROM Stationeries");
        }

        //Отображение всех менеджеров по продажам
        private void button4_Click(object sender, EventArgs e)
        {
            ShowFromDb("SELECT Salesmans.Name, Salesmans.Surname, Sales.DateOfSale FROM Salesmans JOIN Sales ON Sales.SalesmanId = Salesmans.Id");
        }

        //Отображение всех типов канцтоваров
        private void button3_Click(object sender, EventArgs e)
        {
            ShowFromDb("SELECT * FROM StationeryTypes");
        }

        //Показать канцтовары с максимальным количеством единиц;
        private void button5_Click(object sender, EventArgs e)
        {
            ShowFromDb("SELECT Stationeries.Name, Stationeries.Amount FROM Stationeries WHERE Stationeries.Amount = (SELECT MAX(Amount) FROM Stationeries)");
        }

        //Показать канцтовары с минимальным количеством единиц;
        private void button7_Click(object sender, EventArgs e)
        {
            ShowFromDb("SELECT Stationeries.Name, Stationeries.Amount FROM Stationeries WHERE Stationeries.Amount = (SELECT MIN(Amount) FROM Stationeries)");
        }

        //Показать канцтовары с максимальной себестоимостью единицы
        private void button6_Click(object sender, EventArgs e)
        {
            ShowFromDb("SELECT Stationeries.Name, Stationeries.Price FROM Stationeries WHERE Stationeries.Price = (SELECT MAX(Price) FROM Stationeries)");
        }

        //Показать канцтовары с минимальной себестоимостью единицы
        private void button8_Click(object sender, EventArgs e)
        {
            ShowFromDb("SELECT Stationeries.Name, Stationeries.Price FROM Stationeries WHERE Stationeries.Price = (SELECT MIN(Price) FROM Stationeries)");
        }

        private void ShowFromDb(string query)
        {
            using (SqlConnection connection = new(connStr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader? reader = null;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    dt = new();
                    int line = 0;
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dt.Columns.Add(reader.GetName(i));
                            }
                        }
                        DataRow row = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        dt.Rows.Add(row);
                        line++;
                    }
                    dataGridView1.DataSource = dt;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new(connStr)) //Привязываем в comboBox1 типы канцтоваров (StationeryTypes)
            {
                string queryStr = "SELECT * FROM StationeryTypes";
                SqlCommand command = new SqlCommand(queryStr, connection);
                SqlDataReader reader = null;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    types.Clear();
                    while (reader.Read())
                    {
                        StationeryType type = new();
                        type.Id = reader.GetInt32(0);
                        type.Name = reader.GetString(1);
                        types.Add(type);
                    }
                    comboBox1.DataSource = null;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "Id";
                    comboBox1.DataSource = types;
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

        //Показать канцтовары, заданного типа
        private void button9_Click(object sender, EventArgs e)
        {
            ShowFromDb($"SELECT * FROM Stationeries WHERE Stationeries.TypeId = {comboBox1.SelectedValue}");
        }

        //Показать канцтовары, которые купила конкретная фирма покупатель
        private void button10_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new(connStr))
            {
                string title = textBox1.Text;
                string queryStr = $"SELECT Stationeries.Name, Sales.DateOfSale, FirmBuyers.Name AS Firm FROM Stationeries " +
                    $"JOIN Sales ON Sales.StationeryId = Stationeries.Id " +
                    $"JOIN FirmBuyers ON Sales.FirmBuyerId = FirmBuyers.Id " +
                    $"WHERE FirmBuyers.Name = @name";
                SqlCommand command = new SqlCommand(queryStr, connection);
                SqlParameter titleParam = command.Parameters.Add("@name", SqlDbType.NVarChar, 1000);
                titleParam.Value = textBox1.Text;
                SqlDataReader reader = null;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    dt = new();
                    int line = 0;
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dt.Columns.Add(reader.GetName(i));
                            }
                        }
                        DataRow row = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        dt.Rows.Add(row);
                        line++;
                    }
                    dataGridView1.DataSource = dt;
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
    }
}