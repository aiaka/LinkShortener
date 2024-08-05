namespace LinkShortener.Services
{
	public interface IShortenerService
	{
		Task<string> SaveShortened(string host, string url);
		Task<string?> GetUrl(string token);
		Task<string> GenerateToken();
	}
}