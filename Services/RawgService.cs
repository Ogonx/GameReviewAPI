using Newtonsoft.Json;

namespace GameReviewAPI.Services
{
    public class RawgGame
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonProperty("background_image")]
        public string BackgroundImage { get; set; } = string.Empty;

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("genres")]
        public List<RawgGenre> Genres { get; set; } = new();

        [JsonProperty("platforms")]
        public List<RawgPlatformWrapper> Platforms { get; set; } = new();
    }

    public class RawgGenre
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class RawgPlatformWrapper
    {
        [JsonProperty("platform")]
        public RawgPlatform Platform { get; set; } = new();
    }

    public class RawgPlatform
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class RawgSearchResult
    {
        [JsonProperty("results")]
        public List<RawgGame> Results { get; set; } = new();
    }

    public class RawgService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public RawgService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Rawg:ApiKey"] ?? string.Empty;
        }

        public async Task<List<RawgGame>> SearchGamesAsync(string query)
        {
            var url = $"https://api.rawg.io/api/games?key={_apiKey}&search={query}&page_size=10";
            var response = await _httpClient.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<RawgSearchResult>(response);
            return result?.Results ?? new List<RawgGame>();
        }

        public async Task<RawgGame?> GetGameByIdAsync(string rawgId)
        {
            var url = $"https://api.rawg.io/api/games/{rawgId}?key={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<RawgGame>(response);
        }
    }
}