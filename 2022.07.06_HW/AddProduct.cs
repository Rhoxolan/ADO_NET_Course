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
                SqlDataReader reader = null;
                //Ты тут. Воспроизвести то, что на видео (2.14.23)
                try
                {
                    connection.Open();

                    string queryStr = $"SELECT * FROM Types";
                    SqlCommand command = new(queryStr, connection);
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
