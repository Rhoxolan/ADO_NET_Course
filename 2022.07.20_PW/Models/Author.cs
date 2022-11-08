using System;
using System.Collections.Generic;

namespace _2022._07._20_PW.Models;

public partial class Author
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int YearOfBirth { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
