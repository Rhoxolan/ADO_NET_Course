using _2022._07._22_HW.Models;
using Microsoft.EntityFrameworkCore;

namespace _2022._07._22_HW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Вроде как пример явной загрузки - дочитать презентацию и выяснить
            using GamesContext context = new();
            context.Games.Load();
            context.Publishers.Load();
            context.Genres.Load();
            listBox2.DisplayMember = "Name";
            listBox3.DisplayMember = "Name";
            listBox1.DataSource = context.Games.Local.Select(g => $"{g.Name}, {g.Publisher.Name}, {g.Genre.Name}").ToList();
            listBox2.DataSource = context.Publishers.Local.ToList();
            listBox3.DataSource = context.Genres.Local.ToList();

            //Пример безотложной загрузки
            //using GamesContext context = new();
            //var games =
            //    context.Games.Include("Genre").ToList();
            //listBox1.DataSource = games.Select(g => $"{g.Name}, {g.Genre.Name}").ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using GamesContext context = new();
            context.Genres.Load();
            context.Genres.Add(new Genre() { Name = textBox5.Text });
            context.SaveChangesAsync();
            listBox3.DataSource = context.Genres.Local.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using GamesContext context = new();
            context.Publishers.Load();
            context.Publishers.Add(new Publisher() { Name = textBox4.Text });
            context.SaveChangesAsync();
            listBox2.DataSource = context.Publishers.Local.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using GamesContext context = new();
            context.Games.Load();
            context.Publishers.Load();
            context.Genres.Load();

            Game game = new()
            {
                Name = textBox1.Text,
                Genre = context.Genres.Local.Where(g => g.Name == textBox3.Text).FirstOrDefault(),
                Publisher = context.Publishers.Local.Where(p => p.Name == textBox2.Text).FirstOrDefault()
            };

            context.Games.Add(game);
            context.SaveChangesAsync();
            listBox1.DataSource = context.Games.Local.Select(g => $"{g.Name}, {g.Publisher.Name}, {g.Genre.Name}").ToList();
        }
    }
}