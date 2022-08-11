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
    public partial class AddProductProvider : Form
    {
        private string connString;

        public AddProductProvider(string connStr)
        {
            InitializeComponent();
            connString = connStr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new(connString))
            {
                string queryStr = $"Insert into ProductProvider VALUES(N'{textBox1.Text}')";
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
    }
}
