using System.Text.Json;
using DCC.Constants;
using DCC.Models;

namespace DCC.Services;

/// <summary>
///     Provides methods for managing the application's configuration, including creation, retrieval, saving, and deletion.
/// </summary>
internal class ConfigurationService : IConfigurationService
{
    /// <summary>
    ///     The file path for the configuration file stored in the application's data directory.
    /// </summary>
    private readonly string _fileName = Path.Combine(FileSystem.AppDataDirectory, AppConstants.ConfigurationFileName);

    /// <summary>
    ///     JSON serializer options used for configuration file serialization and deserialization.
    /// </summary>
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    ///     Creates a new configuration file if it does not exist, or retrieves the existing configuration.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the configuration object.</returns>
    public async Task<Configuration> CreateConfigurationAsync()
    {
        if (File.Exists(_fileName)) return await GetConfigurationAsync();

        var configuration = new Configuration();
        await SaveConfigurationAsync(configuration);
        return configuration;
    }

    /// <summary>
    ///     Retrieves the configuration from the configuration file.
    ///     If the file does not exist, a new configuration file is created.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the configuration object.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the configuration file cannot be deserialized.</exception>
    public async Task<Configuration> GetConfigurationAsync()
    {
        if (!File.Exists(_fileName)) await CreateConfigurationAsync();

        var json = await File.ReadAllTextAsync(_fileName);
        return JsonSerializer.Deserialize<Configuration>(json, _jsonOptions)
               ?? throw new InvalidOperationException("Failed to deserialize configuration.");
    }

    /// <summary>
    ///     Saves the provided configuration object to the configuration file.
    /// </summary>
    /// <param name="configuration">The configuration object to save.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SaveConfigurationAsync(Configuration configuration)
    {
        try
        {
            var json = JsonSerializer.Serialize(configuration, _jsonOptions);
            await File.WriteAllTextAsync(_fileName, json);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Failed to save configuration.", e);
        }
    }

    /// <summary>
    ///     Deletes the configuration file from the application's data directory.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="Exception">Thrown if the configuration file cannot be deleted.</exception>
    public Task DeleteConfiguration()
    {
        try
        {
            File.Delete(_fileName);
            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to delete configuration.", e);
        }
    }
}