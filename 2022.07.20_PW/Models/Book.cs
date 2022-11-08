using System;
using System.Collections.Generic;

namespace _2022._07._20_PW.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Pages { get; set; }

    public int PublisherId { get; set; }

    public int AuthorId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Publisher Publisher { get; set; } = null!;
}
