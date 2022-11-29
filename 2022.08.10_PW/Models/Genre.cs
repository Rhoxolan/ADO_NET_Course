namespace _2022._08._10_PW.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Game>? Games { get; set; }
    }
}
