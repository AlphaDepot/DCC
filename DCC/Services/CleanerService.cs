using DCC.Models;

namespace DCC.Services;

/// <summary>
///     Manages operations related to cleaners on the back end, including retrieval, creation, updating, and deletion.
/// </summary>
internal class CleanerService : ICleanerService
{
    private readonly IConfigurationService _configurationService;
    private Configuration? _configuration;


    public CleanerService(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    /// <summary>
    ///     Retrieves all cleaners from the configuration.
    ///     Ensures the list of cleaners is initialized if empty.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of cleaners.</returns>
    public async Task<List<Cleaner>> GetCleanersAsync()
    {
        var configuration = await GetConfigurationAsync();

        if (configuration.Cleaners.Count == 0) configuration.Cleaners = [];

        return configuration.Cleaners;
    }

    /// <summary>
    ///     Retrieves a cleaner by its ID.
    ///     Throws an exception if the cleaner is not found.
    /// </summary>
    /// <param name="id">The ID of the cleaner to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the cleaner.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no cleaner with the specified ID is found.</exception>
    public async Task<Cleaner?> GetCleanerByIdAsync(int id)
    {
        var configuration = await GetConfigurationAsync();
        var cleaner = configuration.Cleaners.FirstOrDefault(c => c.Id == id);
        if (cleaner == null) throw new KeyNotFoundException($"Cleaner with ID {id} not found.");

        return await Task.FromResult(cleaner);
    }

    /// <summary>
    ///     Creates a new cleaner and saves it to the configuration.
    ///     Throws an exception if the cleaner is null or if a cleaner with the same ID already exists.
    /// </summary>
    /// <param name="cleaner">The cleaner to create.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created cleaner.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the cleaner is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if a cleaner with the same ID already exists or if creation fails.</exception>
    public async Task<Cleaner> CreateCleanerAsync(Cleaner cleaner)
    {
        try
        {
            if (cleaner == null) throw new ArgumentNullException(nameof(cleaner), "Cleaner cannot be null.");

            var configuration = await GetConfigurationAsync();
            if (configuration.Cleaners.Any(c => c.Id == cleaner.Id))
                throw new InvalidOperationException($"Cleaner with ID {cleaner.Id} already exists.");

            // Set ID if not set
            if (cleaner.Id is 0 or < 0)
                cleaner.Id = configuration.Cleaners.Count != 0 ? configuration.Cleaners.Max(c => c.Id) + 1 : 1;
            // Ensure directories are initialized
            configuration.Cleaners.Add(cleaner);

            await _configurationService.SaveConfigurationAsync(configuration);
            return cleaner;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to create cleaner.", ex);
        }
    }

    /// <summary>
    ///     Updates an existing cleaner and saves the changes to the configuration.
    ///     Throws an exception if the cleaner is null or if the cleaner to update is not found.
    /// </summary>
    /// <param name="cleaner">The cleaner with updated properties.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated cleaner.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the cleaner is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the cleaner to update is not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if updating fails.</exception>
    public async Task<Cleaner> UpdateCleanerAsync(Cleaner cleaner)
    {
        try
        {
            if (cleaner == null) throw new ArgumentNullException(nameof(cleaner), "Cleaner cannot be null.");

            var configuration = await GetConfigurationAsync();
            var existingCleaner = configuration.Cleaners.FirstOrDefault(c => c.Id == cleaner.Id);
            if (existingCleaner == null) throw new KeyNotFoundException($"Cleaner with ID {cleaner.Id} not found.");

            // Update properties
            existingCleaner.Name = cleaner.Name;
            existingCleaner.Description = cleaner.Description;
            existingCleaner.Location = cleaner.Location;
            existingCleaner.Directories = cleaner.Directories;

            await _configurationService.SaveConfigurationAsync(configuration);
            return existingCleaner;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to update cleaner.", ex);
        }
    }

    /// <summary>
    ///     Deletes a cleaner by its ID and saves the changes to the configuration.
    ///     Throws an exception if the cleaner to delete is not found.
    /// </summary>
    /// <param name="id">The ID of the cleaner to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the cleaner to delete is not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if deletion fails.</exception>
    public async Task DeleteCleanerAsync(int id)
    {
        try
        {
            var configuration = await GetConfigurationAsync();

            var cleaner = configuration.Cleaners.FirstOrDefault(c => c.Id == id);
            if (cleaner == null) throw new KeyNotFoundException($"Cleaner with ID {id} not found.");

            configuration.Cleaners.Remove(cleaner);
            await _configurationService.SaveConfigurationAsync(configuration);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to delete cleaner with ID {id}.", ex);
        }
    }

    /// <summary>
    ///     Retrieves the current configuration.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the configuration.</returns>
    private async Task<Configuration> GetConfigurationAsync()
    {
        if (_configuration != null) return _configuration;

        _configuration = await _configurationService.GetConfigurationAsync();
        if (_configuration == null) throw new InvalidOperationException("Configuration could not be loaded.");
        return _configuration;
    }
}