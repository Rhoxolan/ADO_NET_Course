using _2022._07._20_PW.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _2022._07._20_PW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using BooksLibraryContext dBContext = new();
            await dBContext.Books.LoadAsync();
            await dBContext.Authors.LoadAsync();
            await dBContext.Publishers.LoadAsync();
            var books = dBContext.Books.Local;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = books.Select(t => new { t.Title, Author = $"{t.Author.Firstname} {t.Author.Surname}", Publisher = t.Publisher.Name, t.Pages }).ToList();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            using BooksLibraryContext dBContext = new();
            await dBContext.Books.LoadAsync();
            await dBContext.Authors.LoadAsync();
            await dBContext.Publishers.LoadAsync();
            Book book = new()
            {
                Title = textBox1.Text,
                Author = dBContext.Authors.Local.Where(a => a.Firstname == textBox3.Text && a.Surname == textBox4.Text).FirstOrDefault()!,
                Pages = (int)numericUpDown1.Value,
                Publisher = dBContext.Publishers.Local.Where(p => p.Name == textBox2.Text).FirstOrDefault()!
            };
            dBContext.Books.Add(book);
            await dBContext.SaveChangesAsync();
            var books = dBContext.Books.Local;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = books.Select(t => new { t.Title, Author = $"{t.Author.Firstname} {t.Author.Surname}", Publisher = t.Publisher.Name, t.Pages }).ToList();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                using BooksLibraryContext dBContext = new();
                await dBContext.Books.LoadAsync();
                await dBContext.Authors.LoadAsync();
                await dBContext.Publishers.LoadAsync();
                Book book = dBContext.Books.Local.Where(a => a.Title == dataGridView1.SelectedRows[0].Cells["Title"].Value.ToString()).FirstOrDefault()!;
                if (book != null)
                {
                    book.Title = textBox8.Text;
                    book.Author = dBContext.Authors.Local.Where(a => a.Firstname == textBox6.Text && a.Surname == textBox5.Text).FirstOrDefault()!;
                    book.Pages = (int)numericUpDown2.Value;
                    book.Publisher = dBContext.Publishers.Local.Where(p => p.Name == textBox7.Text).FirstOrDefault()!;
                    await dBContext.SaveChangesAsync();
                    var books = dBContext.Books.Local;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = books.Select(t => new { t.Title, Author = $"{t.Author.Firstname} {t.Author.Surname}", Publisher = t.Publisher.Name, t.Pages }).ToList();
                }
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                using BooksLibraryContext dBContext = new();
                await dBContext.Books.LoadAsync();
                await dBContext.Authors.LoadAsync();
                await dBContext.Publishers.LoadAsync();
                Book book = dBContext.Books.Local.Where(a => a.Title == dataGridView1.SelectedRows[0].Cells["Title"].Value.ToString()).FirstOrDefault()!;
                if (book != null)
                {
                    dBContext.Books.Remove(book);
                    await dBContext.SaveChangesAsync();
                    var books = dBContext.Books.Local;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = books.Select(t => new { t.Title, Author = $"{t.Author.Firstname} {t.Author.Surname}", Publisher = t.Publisher.Name, t.Pages }).ToList();
                }
            }
        }
    }
}