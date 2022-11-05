using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2022._07._18_PW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (BooksLibraryEntities context = new BooksLibraryEntities())
            {
                await context.Books.LoadAsync();
                var books = context.Books.Local;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = books.Select(t => new {t.Title, Author = $"{t.Author.Firstname} {t.Author.Surname}", Publisher = t.Publisher.Name, t.Pages}).ToList();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            using (BooksLibraryEntities context = new BooksLibraryEntities())
            {
                await context.Publishers.LoadAsync();
                var publishers = context.Publishers.Local;
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = publishers.Select(t => new { t.Name }).ToList();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            using (BooksLibraryEntities context = new BooksLibraryEntities())
            {
                Publisher publisher = new Publisher()
                {
                    Name = textBox1.Text
                };
                context.Publishers.Add(publisher);
                await context.SaveChangesAsync();
                await context.Publishers.LoadAsync();
                var publishers = context.Publishers.Local;
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = publishers.Select(t => new { t.Name }).ToList();
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            using (BooksLibraryEntities context = new BooksLibraryEntities())
            {
                await context.Authors.LoadAsync();
                var authors = context.Authors.Local;
                dataGridView3.DataSource = null;
                dataGridView3.DataSource = authors.Select(t => new { t.Firstname, t.Surname, t.YearOfBirth }).ToList();
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            using (BooksLibraryEntities context = new BooksLibraryEntities())
            {
                Author author = new Author()
                {
                    Firstname = textBox2.Text,
                    Surname = textBox3.Text,
                    YearOfBirth = Convert.ToInt32(numericUpDown1.Value)
                };
                context.Authors.Add(author);
                await context.SaveChangesAsync();
                await context.Authors.LoadAsync();
                var authors = context.Authors.Local;
                dataGridView3.DataSource = null;
                dataGridView3.DataSource = authors.Select(t => new { t.Firstname, t.Surname, t.YearOfBirth }).ToList();
            }
        }
    }
}
