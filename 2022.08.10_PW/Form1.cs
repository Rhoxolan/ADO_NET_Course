using _2022._08._10_PW.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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

            Sale sale = new Sale
            {
                Date = DateTime.Now,
                Game = await context.Games.FindAsync(1)
            };
            await context.SaveChangesAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Показать топ-3 студий с максимальным количеством игр;

            using GamesContext context = new();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await context.ShowTop3PublishersPerGamesCollection.ToListAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //Показать студию с максимальным количеством игр;

            using GamesContext context = new();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await context.ShowTopPublisherPerGamesCollection.ToListAsync();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // Показать топ-3 самых популярных стилей по количеству игр;

            using GamesContext context = new();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await context.ShowTopGenresPerGamesCollection.ToListAsync();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            //Показать самый популярный стиль по количеству игр.

            using GamesContext context = new();
            dataGridView1.DataSource = null;
            var topGenresPerGames = await context.ShowTopGenresPerGamesCollection.ToArrayAsync();
            dataGridView1.DataSource = new ArrayList() { topGenresPerGames.First() };
        }
    }
}