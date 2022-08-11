using System.Data;
using System.Data.SqlClient;

namespace _2022._07._06_HW
{
    public partial class Form1 : Form
    {
        private string connString;
        private DataTable dt;

        public Form1()
        {
            connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Warehouse;Integrated Security=True";
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadFromDB("SELECT * FROM Products", dataGridView1);
            LoadFromDB("SELECT * FROM Types", dataGridView2);
            LoadFromDB("SELECT * FROM ProductProvider", dataGridView3);
        }

        private void LoadFromDB(string queryStr, DataGridView dataGridView)
        {
            using (SqlConnection connection = new(connString))
            {
                SqlDataReader? reader = null;
                dt = new();
                SqlCommand cmd = new SqlCommand(queryStr, connection);
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();
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
                        line++;
                        DataRow row = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        dt.Rows.Add(row);
                    }
                    dataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddType ad = new(connString);
            ad.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddProductProvider app = new(connString);
            app.ShowDialog();
        }
    }
}