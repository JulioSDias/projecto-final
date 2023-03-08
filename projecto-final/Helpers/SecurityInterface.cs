namespace SodokuAPI.Helpers
{
    public interface ISecurity
    {
        Byte[] CreatePasswordSalt(string password);
        Byte[] CreatePasswordHash(string password, byte[] salt);
        Boolean VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
    }
}
