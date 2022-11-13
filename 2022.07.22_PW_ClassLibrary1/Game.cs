namespace _2022._07._22_PW_ClassLibrary1
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Publisher { get; set; } = String.Empty;

        public GameGenre Genre { get; set; } = GameGenre.None;

        public override string ToString()
        {
            return $"{Name}, {Publisher}, {Genre}";
        }
    }
}