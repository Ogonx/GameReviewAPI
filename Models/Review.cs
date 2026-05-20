namespace GameReviewAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}