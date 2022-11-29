using _2022._08._10_PW.Models;
using Microsoft.EntityFrameworkCore;

namespace _2022._08._10_PW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            using GamesContext context = new();
            await context.Database.EnsureCreatedAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using GamesContext context = new();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await context.ShowTop3PublishersPerGamesCollection.ToListAsync();
        }
    }
}