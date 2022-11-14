namespace _2022._07._22_HW.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public int PublisherId { get; set; }

        public int GenreId { get; set; }

        public virtual Publisher Publisher { get; set; } = null!;

        public virtual Genre Genre { get; set; } = null!;

        public override string ToString()
        {
            return $"{Name}, {Publisher}, {Genre}";
        }
    }
}
