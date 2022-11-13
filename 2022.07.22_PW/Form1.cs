using _2022._07._22_PW_ClassLibrary1;
using _2022._07._22_PW_ClassLibrary2;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace _2022._07._22_PW
{
    public partial class Form1 : Form
    {
        BindingList<Game> games;

        public Form1()
        {
            InitializeComponent();
            games = new();
            listBox1.DataSource = games;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using GamesContext context = new();
            context.Database.EnsureCreated();
            context.Games.Load();
            games = context.Games.Local.ToBindingList();
        }


    }
}