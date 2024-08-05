using LinkShortener.Entities;

namespace LinkShortener.Repositories
{
	public interface IShortenedUrlRepository
	{
		Task AddShortenedUrl(ShortenedUrl url);
		Task<ShortenedUrl?> GetShortenedUrl(string token);

	}
}