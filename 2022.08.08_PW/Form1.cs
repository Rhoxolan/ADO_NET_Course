using _2022._08._08_PW.Models;

namespace _2022._08._08_PW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using GamesContext context = new();
            await context.Genres.AddAsync(new Genre { Name = textBox1.Text });
            await context.SaveChangesAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            using GamesContext context = new();
            await context.Сountries.AddAsync(new Country { Name = textBox2.Text });
            await context.SaveChangesAsync();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            using GamesContext context = new();
            
            City city = new City { Name = textBox3.Text, Country = context.Сountries.Where(c => c.Name == textBox4.Text).FirstOrDefault()! };

            await context.Cities.AddAsync(city);
            await context.SaveChangesAsync();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            using GamesContext context = new();

            Publisher publisher = new Publisher
            {
                Name = textBox5.Text,
                City = context.Cities.Where(c => c.Name == textBox6.Text).FirstOrDefault()!
            };

            await context.Publishers.AddAsync(publisher);
            await context.SaveChangesAsync();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            using GamesContext context = new();

            Game game = new Game
            {
                Name = textBox7.Text,
                Publisher = context.Publishers.Where(p => p.Name == textBox8.Text).FirstOrDefault()!,
                Genre = context.Genres.Where(g => g.Name == textBox9.Text).FirstOrDefault()!
            };

            await context.Games.AddAsync(game);
            await context.SaveChangesAsync();
        }
    }
}