namespace RefitClientGeneratedModels.Extensions;

public static class SemaphoreSlimExtensions
{
    public static async Task<IDisposable> CreateDisposableLockAsync(this SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync();
        return new SemaphoreSlimLock(semaphore);
    }
}

internal sealed class SemaphoreSlimLock(SemaphoreSlim semaphore) : IDisposable
{
    public void Dispose() => semaphore.Release();
}
