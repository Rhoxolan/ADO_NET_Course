using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2022._08._08_PW
{
    public partial class FormTopGenres : Form
    {
        public FormTopGenres(DataGridView dataGridView)
        {
            InitializeComponent();
            this.dataGridView1.DataSource = dataGridView.DataSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
