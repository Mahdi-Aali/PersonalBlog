using System.Text;
using System.Text.Json;

namespace PersonalBlog.BuildingBlocks.Logging;

public record Log
{
    public LogType LogType { get; private set; }

    /// <summary>
    /// Exception ID
    /// </summary>
    public string EXID { get; private set; } = string.Empty;

    public string Message { get; private set; } = string.Empty;

    public DateTime IssuedDateTime { get; private set; }

    public string CallStack { get; private set; } = string.Empty;

    public string Exception { get; private set; } = string.Empty;


    public Log(LogType logType, string message, string? exId, Exception? ex)
    {
        LogType = logType;
        Message = message;
        EXID = exId is not null ? exId : "-";
        IssuedDateTime = DateTime.Now;
        CallStack = "-";
        if (ex is not null)
        {
            CallStack = ex.StackTrace ?? "-";
            StringBuilder exStringBuilder = new();
            exStringBuilder.AppendLine($"Exception message:\n {ex.Message}");
            exStringBuilder.AppendLine($"Exception call stack:\n {ex.StackTrace}");
            exStringBuilder.AppendLine($"Exception inner exception:\n {ex.InnerException}");
            exStringBuilder.AppendLine($"Exception source:\n {ex.Source}");
            exStringBuilder.AppendLine($"Exception data:\n {JsonSerializer.Serialize(ex.Data)}");
            Exception = exStringBuilder.ToString();
        }
        else
        {
            Exception = "-";
        }
    }
}

public enum LogType
{
    Information,
    Warning,
    Error
}


