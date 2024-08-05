using LinkShortener.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Repositories
{
	public class ShortenedUrlRepository(ApplicationDBContext dbContext) : IShortenedUrlRepository
	{
		ApplicationDBContext _dbContext = dbContext;
		public async Task AddShortenedUrl(ShortenedUrl url)
		{
			_dbContext.ShortenedUrls.Add(url);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<ShortenedUrl?> GetShortenedUrl(string token)
		{
			return await _dbContext.ShortenedUrls.FirstOrDefaultAsync(url => url.Token == token);
		}
	}
}
