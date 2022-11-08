using System;
using System.Collections.Generic;

namespace _2022._07._20_PW.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
