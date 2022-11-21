namespace _2022._08._08_PW.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual City City { get; set; } = null!;

        public virtual ICollection<Game>? Games { get; set; }
    }
}
