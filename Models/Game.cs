namespace GameReviewAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string RawgId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string BackgroundImage { get; set; } = string.Empty;
        public string Genres { get; set; } = string.Empty;
        public string Platforms { get; set; } = string.Empty;
        public double RawgRating { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}