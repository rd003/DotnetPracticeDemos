using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace AzureRedisDemo.Extensions;

public static class DistributedCacheExtensoion
{
    public static async Task SetCacheAsync<T>(this IDistributedCache cache, string key, T data, TimeSpan? absoulteExpireTime = null, TimeSpan? slidingExpireTime = null)
    {
        DistributedCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = absoulteExpireTime ?? TimeSpan.FromSeconds(60),
            SlidingExpiration = slidingExpireTime
        };
        string serializedData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(key, serializedData, options);
    }

    public static async Task<T?> GetCacheAsync<T>(this IDistributedCache cache, string key)
    {
        var serializedData = await cache.GetStringAsync(key);
        if (serializedData == null)
        {
            return default;
        }
        T data = JsonSerializer.Deserialize<T>(serializedData);
        return data;
    }

}