namespace _2022._08._08_PW.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Publisher>? Publishers { get; set; }
    }
}