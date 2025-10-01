// DebugLogger.cs
using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

public class DebugLogger
{
    private readonly ISessionStorageService _storage;
    private const string LogKey = "DebugLogs";

    public DebugLogger(ISessionStorageService storage)
    {
        _storage = storage;
    }

    public async Task LogAsync(string message)
    {
        var logs = await _storage.GetItemAsync<List<string>>(LogKey) ?? new List<string>();
        logs.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
        await _storage.SetItemAsync(LogKey, logs);
    }

    public async Task<List<string>> GetLogsAsync()
    {
        return await _storage.GetItemAsync<List<string>>(LogKey) ?? new List<string>();
    }

    public async Task ClearLogsAsync()
    {
        await _storage.RemoveItemAsync(LogKey);
    }
}
