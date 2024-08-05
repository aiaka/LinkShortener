namespace LinkShortener.Entities
{
	public class ShortenedUrl
	{
		public Guid Id { get; set; }
		public string LongUrl { get; set; } = string.Empty;
		public string ShortUrl { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public DateTime CreatedOnUTC { get; set; }

	}
}
