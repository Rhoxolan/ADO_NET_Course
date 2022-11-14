﻿namespace _2022._07._22_HW.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Game>? Games { get; set; }
    }
}
