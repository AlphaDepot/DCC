using DCC.Models;

namespace DCC.Services;

/// <summary>
///     Provides methods for managing application configuration, including creation, retrieval, saving, and deletion.
/// </summary>
public interface IConfigurationService
{
    Task<Configuration> CreateConfigurationAsync();
    Task<Configuration> GetConfigurationAsync();
    Task SaveConfigurationAsync(Configuration configuration);
    Task DeleteConfiguration();
}