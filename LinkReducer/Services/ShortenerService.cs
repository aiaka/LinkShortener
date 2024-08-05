using LinkShortener.Caching;
using LinkShortener.Entities;
using LinkShortener.Helpers;
using LinkShortener.Repositories;

namespace LinkShortener.Services
{
	public class ShortenerService(IShortenedUrlRepository shortenedUrlRepository, ITokenCache tokenCache) : IShortenerService
	{
		private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		private readonly Random _random = new();
		private readonly IShortenedUrlRepository _shortenedUrlRepository = shortenedUrlRepository;
		private readonly ITokenCache _tokenCache = tokenCache;

		public async Task<string> SaveShortened(string host, string url)
		{
			// Generate token logic here
			var token = _tokenCache.GetToken();

			var shortenedUrl = new ShortenedUrl
			{
				Id = Guid.NewGuid(),
				LongUrl = url,
				Token = token ?? await GenerateToken(),
				ShortUrl = $"{host}/{token}",
				CreatedOnUTC = DateTime.Now
			};
			await _shortenedUrlRepository.AddShortenedUrl(shortenedUrl);

			return shortenedUrl.ShortUrl;
		}
		public async Task<string?> GetUrl(string token)
		{
			return (await _shortenedUrlRepository.GetShortenedUrl(token))?.LongUrl;
		}

		public async Task<string> GenerateToken()
		{
			var codeChars = new char[AppConstants.NumberOfTokenChars];
			while (true)
			{
				var tokenChars = new char[AppConstants.NumberOfTokenChars];

				for (var i = 0; i < AppConstants.NumberOfTokenChars; i++)
				{
					var randomIndex = _random.Next(Alphabet.Length - 1);

					tokenChars[i] = Alphabet[randomIndex];
				}
				var token = new string(tokenChars);
				if (await _shortenedUrlRepository.GetShortenedUrl(token) == null)
				{
					return token;
				}
			}
		}
	}
}
