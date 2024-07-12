using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PersonalBlog.BuildingBlocks.Logging;

public class DefaultLoggerWithElastic : Contracts.ILogger
{
    private readonly ILogger<DefaultLoggerWithElastic> _logger;
    private readonly ElasticsearchClient _elasticSearchClient;
    private readonly string _elasticIndexName;
    

    public DefaultLoggerWithElastic(ILogger<DefaultLoggerWithElastic> logger, ElasticsearchClient elasticsearchClient, IConfiguration configuration)
    {
        _logger = logger;
        _elasticSearchClient = elasticsearchClient;
        _elasticIndexName = configuration["ElasticLogIndexName"] ?? "DefaultLog";
    }

    public virtual async Task<Log> LogError(Exception? ex, string message)
    {
        Log log = new(LogType.Error, message, Guid.NewGuid().ToString().Replace("-", ""), ex);

        await SendLogToElastic(log);

        _logger.LogError(ex, message, [new KeyValuePair<string, string>("EXID", log.EXID)]);

        return log;
    }

    public virtual async Task<Log> LogInformation(string message)
    {
        Log log = new(LogType.Information, message, null, null);

        await SendLogToElastic(log);

        _logger.LogInformation(message);

        return log;
    }

    public virtual async Task<Log> LogWarning(string message)
    {
        Log log = new(LogType.Warning, message, null, null);

        await SendLogToElastic(log);

        _logger.LogWarning(message);

        return log;
    }


    private async Task SendLogToElastic(Log log)
    {
        try
        {
            var response = await _elasticSearchClient.IndexAsync(log, idx => idx.Index(_elasticIndexName));
            if (!response.IsSuccess())
            {
                throw new Exception("Faild when comunicating with elastic.");
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "something went wrong when comunication with elastic search engin.");
        }
    }
}
