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
                dataGridView1.DataSource = books.Select(t => new { t.Title, Author = $"{t.Author.Firstname} {t.Author.Surname}", Publisher = t.Publisher.Name, t.Pages }).ToList();
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

        private async void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                using (BooksLibraryEntities context = new BooksLibraryEntities())
                {
                    await context.Publishers.LoadAsync();
                    Publisher publisher = context.Publishers.Local.Where(p => p.Name == dataGridView2.SelectedRows[0].Cells["Name"].Value.ToString()).FirstOrDefault();
                    if (publisher != null)
                    {
                        context.Publishers.Remove(publisher);
                        await context.SaveChangesAsync();
                        var publishers = context.Publishers.Local;
                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = publishers.Select(t => new { t.Name }).ToList();
                    }
                }
            }
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                using (BooksLibraryEntities context = new BooksLibraryEntities())
                {
                    await context.Authors.LoadAsync();
                    Author author = context.Authors.Local.Where(a => a.Firstname == dataGridView3.SelectedRows[0].Cells["Firstname"].Value.ToString() &&
                    a.Surname == dataGridView3.SelectedRows[0].Cells["Surname"].Value.ToString()).FirstOrDefault();
                    if (author != null)
                    {
                        context.Authors.Remove(author);
                        await context.SaveChangesAsync();
                        var authors = context.Authors.Local;
                        dataGridView3.DataSource = null;
                        dataGridView3.DataSource = authors.Select(t => new { t.Firstname, t.Surname, t.YearOfBirth }).ToList();
                    }
                }
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                using (BooksLibraryEntities context = new BooksLibraryEntities())
                {
                    await context.Books.LoadAsync();
                    Book book = context.Books.Local.Where(a => a.Title == dataGridView1.SelectedRows[0].Cells["Title"].Value.ToString()).FirstOrDefault();
                    if (book != null)
                    {
                        context.Books.Remove(book);
                        await context.SaveChangesAsync();
                        var books = context.Books.Local;
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = books.Select(t => new { t.Title, Author = $"{t.Author.Firstname} {t.Author.Surname}", Publisher = t.Publisher.Name, t.Pages }).ToList();
                    }
                }
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            using (BooksLibraryEntities context = new BooksLibraryEntities())
            {
                await context.Authors.LoadAsync();
                await context.Publishers.LoadAsync();
                await context.Books.LoadAsync();
                Book book = new Book()
                {
                    Title = textBox4.Text,
                    Author = context.Authors.Local.Where(a => a.Firstname == textBox6.Text && a.Surname == textBox7.Text).FirstOrDefault(),
                    Pages = (int)numericUpDown2.Value,
                    Publisher = context.Publishers.Local.Where(p => p.Name == textBox5.Text).FirstOrDefault()
                };
                context.Books.Add(book);
                await context.SaveChangesAsync();
                var books = context.Books.Local;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = books.Select(t => new { t.Title, Author = $"{t.Author.Firstname} {t.Author.Surname}", Publisher = t.Publisher.Name, t.Pages }).ToList();
            }
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                using (BooksLibraryEntities context = new BooksLibraryEntities())
                {
                    await context.Publishers.LoadAsync();
                    Publisher publisher = context.Publishers.Local.Where(p => p.Name == dataGridView2.SelectedRows[0].Cells["Name"].Value.ToString()).FirstOrDefault();
                    if (publisher != null)
                    {
                        publisher.Name = textBox8.Text;
                        await context.SaveChangesAsync();
                        var publishers = context.Publishers.Local;
                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = publishers.Select(t => new { t.Name }).ToList();
                    }
                }
            }
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                using (BooksLibraryEntities context = new BooksLibraryEntities())
                {
                    await context.Authors.LoadAsync();
                    Author author = context.Authors.Local.Where(a => a.Firstname == dataGridView3.SelectedRows[0].Cells["Firstname"].Value.ToString() &&
                    a.Surname == dataGridView3.SelectedRows[0].Cells["Surname"].Value.ToString()).FirstOrDefault();
                    if (author != null)
                    {
                        author.Firstname = textBox9.Text;
                        author.Surname = textBox10.Text;
                        author.YearOfBirth = (int)numericUpDown3.Value;
                        await context.SaveChangesAsync();
                        var authors = context.Authors.Local;
                        dataGridView3.DataSource = null;
                        dataGridView3.DataSource = authors.Select(t => new { t.Firstname, t.Surname, t.YearOfBirth }).ToList();
                    }
                }
            }
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                using (BooksLibraryEntities context = new BooksLibraryEntities())
                {
                    await context.Books.LoadAsync();
                    Book book = context.Books.Local.Where(a => a.Title == dataGridView1.SelectedRows[0].Cells["Title"].Value.ToString()).FirstOrDefault();
                    if (book != null)
                    {
                        book.Title = textBox14.Text;
                        book.Author = context.Authors.Local.Where(a => a.Firstname == textBox12.Text && a.Surname == textBox11.Text).FirstOrDefault();
                        book.Pages = (int)numericUpDown4.Value;
                        book.Publisher = context.Publishers.Local.Where(p => p.Name == textBox13.Text).FirstOrDefault();
                        await context.SaveChangesAsync();
                        var books = context.Books.Local;
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = books.Select(t => new { t.Title, Author = $"{t.Author.Firstname} {t.Author.Surname}", Publisher = t.Publisher.Name, t.Pages }).ToList();
                    }
                }
            }
        }
    }
}
