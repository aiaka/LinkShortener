using LinkShortener.Services;

namespace LinkShortener.Caching
{
	public class TokenGeneratorService(ITokenCache memoryCache,
								IServiceProvider serviceProvider) : BackgroundService
	{
		private const int ExecutionPeriodSec = 10;
		private const int CacheCount = 100;
		private readonly ITokenCache _memoryCache = memoryCache;
		private readonly IServiceProvider _serviceProvider = serviceProvider;

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				if (_memoryCache.Count() < CacheCount)
				{
					using (var scope = _serviceProvider.CreateScope())
					{
						var shortenerService = scope.ServiceProvider.GetRequiredService<IShortenerService>();
						var newValue = await shortenerService.GenerateToken();
						_memoryCache.AddToken(newValue);
					}
				}

				await Task.Delay(TimeSpan.FromSeconds(ExecutionPeriodSec), stoppingToken);
			}
		}
	}
}
