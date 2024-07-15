namespace PersonalBlog.BuildingBlocks.Logging.Contracts;

public interface ILogger
{
    public abstract Task<Log> LogInformation(string message);
    public abstract Task LogInformation<TLog>(TLog log);
    public abstract Task<Log> LogWarning(string message);
    public abstract Task<Log> LogError(Exception? ex, string message);
}
