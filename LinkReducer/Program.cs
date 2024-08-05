using LinkShortener;
using LinkShortener.Caching;
using LinkShortener.Models;
using LinkShortener.Repositories;
using LinkShortener.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(x =>
		x.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

builder.Services.AddScoped<IShortenerService, ShortenerService>();
builder.Services.AddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();
builder.Services.AddSingleton<ITokenCache, TokenCache>();
builder.Services.AddHostedService<TokenGeneratorService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.MapPost("shorturl", async (
	UrlShortenerRequest request,
	IShortenerService shortenerService,
	HttpContext httpContext) =>
{
	if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
	{
		return Results.BadRequest("The specify URL is invalid");
	}
	var shortUrl = await shortenerService.SaveShortened($"{httpContext.Request.Scheme}://{httpContext.Request.Host}", request.Url);
	return Results.Ok(shortUrl);

});

app.MapGet("{token}", async (string token, IShortenerService shortenerService) =>
{
	var shortenedUrl = await shortenerService.GetUrl(token);
	if (shortenedUrl == null)
	{
		return Results.NotFound();
	}
	return Results.Redirect(shortenedUrl);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
