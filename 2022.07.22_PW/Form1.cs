using _2022._07._22_PW_ClassLibrary1;
using _2022._07._22_PW_ClassLibrary2;
using Microsoft.EntityFrameworkCore;

namespace _2022._07._22_PW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using GamesContext context = new();
            context.Database.EnsureCreated();
            context.Games.Load();
            listBox1.DataSource = context.Games.Local.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game game = new()
            {
                Name = textBox1.Text,
                Publisher = textBox2.Text,
                Genre = (GameGenre)(int)numericUpDown1.Value
            };
            using GamesContext context = new();
            context.Games.Load();
            context.Entry(game).State = EntityState.Added; //Пример "Тонкой настройки"
            context.SaveChanges();
            listBox1.DataSource = context.Games.Local.ToList();
        }
    }
}