namespace PersonalBlog.BuildingBlocks.Extenssions.Tasks;

public static class TaskExtenssions
{
    public static async Task WithTimeout(this Task task, TimeSpan timeout)
    {
        CancellationTokenSource cancellationTokenSource = new();
        Task resultTask = await Task.WhenAny(task, Task.Delay(timeout, cancellationTokenSource.Token));
        if (resultTask != task)
        {
            TimeoutException(timeout);
        }
        await task;
        cancellationTokenSource.Cancel();
    }

    public static async Task<TResult> WithTimeout<TResult>(this Task<TResult> task, TimeSpan timeout)
    {
        CancellationTokenSource cancellationTokenSource = new();
        Task winnerTask = await Task.WhenAny(task, Task.Delay(timeout, cancellationTokenSource.Token));
        if(winnerTask != task)
        {
            TimeoutException(timeout);
        }
        cancellationTokenSource.Cancel();
        return await task;
    }

    private static void TimeoutException(TimeSpan timeout)
    {
        throw new TimeoutException($"Task timedout. specified timeout: {timeout.TotalSeconds} second(s).");
    }
}