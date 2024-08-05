using LinkShortener.Entities;
using LinkShortener.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener
{
	public class ApplicationDBContext(DbContextOptions options) : DbContext(options)
	{
		public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ShortenedUrl>(builder =>
			{
				builder.Property(x => x.Token).HasMaxLength(AppConstants.NumberOfTokenChars);
				builder.HasIndex(x => x.Token).IsUnique();
			});
		}
	}

}
