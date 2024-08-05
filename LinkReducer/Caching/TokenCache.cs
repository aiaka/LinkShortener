using System.Collections.Concurrent;

namespace LinkShortener.Caching
{

	public class TokenCache : ITokenCache
	{
		private ConcurrentStack<string> cache;

		public TokenCache() => cache = new ConcurrentStack<string>();

		public void AddToken(string newString)
		{
			if (!cache.Contains(newString))
			{
				cache.Push(newString);
			}
		}

		public string? GetToken()
		{
			return cache.TryPop(out var token) ? token : null;
		}

		public int Count()
		{
			return cache.Count;
		}
	}
}