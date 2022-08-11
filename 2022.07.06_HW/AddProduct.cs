using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2022._07._06_HW
{
    public partial class AddProduct : Form
    {
        private string connString;

        public AddProduct(string connStr)
        {
            InitializeComponent();
            connString = connStr;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new(connString))
            {
                string queryStr = $"Insert into Products VALUES(" +
                    $"{comboBox1.SelectedValue}, " +
                    $"{comboBox2.SelectedValue}, " +
                    $"N'{textBox1.Text}', " +
                    $"{numericUpDown3.Value}, " +
                    $"{numericUpDown4.Value}, " +
                    $"'{dateTimePicker1.Value.ToString("MM-dd-yyyy")}')";
                SqlCommand command = new(queryStr, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new(connString))
            {
                SqlDataReader reader = null;
                SqlCommand cmd = null;
                List<Type> types = new List<Type>();
                List<ProductProvider> providers = new List<ProductProvider>();
                try
                {
                    connection.Open();
                    cmd = new("SELECT * FROM Types", connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        types.Add(new Type { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                    comboBox1.DataSource = null;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "Id";
                    comboBox1.DataSource = types;
                    connection.Close();

                    connection.Open();
                    cmd = new("SELECT * FROM ProductProvider", connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        providers.Add(new ProductProvider { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                    comboBox2.DataSource = null;
                    comboBox2.DisplayMember = "Name";
                    comboBox2.ValueMember = "Id";
                    comboBox2.DataSource = providers;
                    connection.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
