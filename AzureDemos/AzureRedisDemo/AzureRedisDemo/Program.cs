using AzureRedisDemo.Extensions;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
 {
     options.Configuration = builder.Configuration.GetConnectionString("Redis");
     options.InstanceName = "AzureRedisDemo_";
 });
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();


app.MapGet("/", async (IDistributedCache cache) =>
{
    string cacheKey = "currentTime";
    DateTime? now = await cache.GetCacheAsync<DateTime>(cacheKey);

    if (now == DateTime.MinValue)
    {
        now = DateTime.Now;
        await cache.SetCacheAsync(cacheKey, now);
    }
    return Results.Ok(now.Value.ToString("yyyy-MM-dd HH:mm:ss"));
});

app.Run();
