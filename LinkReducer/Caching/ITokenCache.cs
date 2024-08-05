namespace LinkShortener.Caching
{
    public interface ITokenCache
    {
        void AddToken(string newString);
        string? GetToken();
        int Count();
    }
}
