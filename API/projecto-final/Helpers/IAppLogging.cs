namespace Projecto_Final.Helpers
{
    public interface IAppLogging
    {
        bool LogInfo(string message);
        bool LogWarning(string message);
        bool LogError(string message);

    }
}
