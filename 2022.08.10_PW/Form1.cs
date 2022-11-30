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

        private async void button5_Click(object sender, EventArgs e)
        {
            //Показать топ-3 самых популярных стилей по количеству продаж;

            using GamesContext context = new();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = await context.ShowTop3StylesPerSalesCollection.ToListAsync();
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            //Показать самый популярный стиль по количеству продаж;

            using GamesContext context = new();
            var topStylePerSale = await context.ShowTop3StylesPerSalesCollection.ToArrayAsync();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new ArrayList() { topStylePerSale.First() };
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            //Показать самую популярную игру по количеству продаж

            using GamesContext context = new();

            //Вариант 1
            var topGamePerSales = await context.Games.OrderByDescending(x => x.Sales!.Count()).FirstAsync();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new ArrayList() { topGamePerSales };

            //Вариант 2
            var gameTop = await context.Games.GroupJoin(context.Sales,
                game => game.Id,
                sale => sale.GameId,
                (game, sales) => new
                {
                    game.Name,
                    Sales = sales.Count()
                }).ToListAsync();
            var game = gameTop.OrderByDescending(x => x.Sales).First();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new ArrayList() { game };
        }
    }
}