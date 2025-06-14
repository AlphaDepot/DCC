using DCC.Models;
using DCC.Services;

namespace DCC.Managers;

/// <summary>
///     Manages operations related to cleaners, including loading, adding, updating, and removing cleaners.
/// </summary>
public class CleanerManager : ICleanerManager
{
    private readonly ICleanerService _cleanerService;

    public CleanerManager(ICleanerService cleanerService)
    {
        _cleanerService = cleanerService;
    }

    /// <summary>
    ///     Event triggered when the state of the cleaners changes.
    /// </summary>
    public Action? OnChange { get; set; }

    /// <summary>
    ///     Gets the list of cleaners currently managed.
    /// </summary>
    public List<Cleaner> Cleaners { get; private set; } = [];

    /// <summary>
    ///     Loads the list of cleaners asynchronously, optionally forcing a refresh.
    /// </summary>
    /// <param name="forceRefresh">If true, forces a refresh of the cleaner list even if it is already loaded.</param>
    public async Task LoadCleanersAsync(bool forceRefresh = false)
    {
        if (Cleaners.Count == 0 || forceRefresh) Cleaners = await _cleanerService.GetCleanersAsync();

        NotifyStateChanged();
    }

    /// <summary>
    ///     Adds a new cleaner asynchronously.
    /// </summary>
    /// <param name="cleaner">The cleaner to add.</param>
    public async Task AddCleanerAsync(Cleaner cleaner)
    {
        await _cleanerService.CreateCleanerAsync(cleaner);
        NotifyStateChanged();
    }

    /// <summary>
    ///     Updates an existing cleaner asynchronously.
    /// </summary>
    /// <param name="cleaner">The cleaner with updated information.</param>
    /// <exception cref="InvalidOperationException">Thrown if the cleaner does not exist.</exception>
    public async Task UpdateCleanerAsync(Cleaner cleaner)
    {
        ArgumentNullException.ThrowIfNull(cleaner);

        var existingCleaner = Cleaners.FirstOrDefault(c => c.Id == cleaner.Id)
                              ?? throw new InvalidOperationException($"Cleaner with ID {cleaner.Id} does not exist.");

        // Update the existing cleaner's properties
        existingCleaner.Name = cleaner.Name;
        existingCleaner.Description = cleaner.Description;
        existingCleaner.Location = cleaner.Location;
        existingCleaner.Directories = cleaner.Directories;

        await _cleanerService.UpdateCleanerAsync(cleaner);
        NotifyStateChanged();
    }

    /// <summary>
    ///     Removes a cleaner asynchronously by its ID.
    /// </summary>
    /// <param name="id">The ID of the cleaner to remove.</param>
    /// <exception cref="InvalidOperationException">Thrown if the cleaner does not exist.</exception>
    public async Task RemoveCleanerAsync(int id)
    {
        var cleaner = Cleaners.FirstOrDefault(c => c.Id == id);
        if (cleaner == null) throw new InvalidOperationException($"Cleaner with ID {id} does not exist.");

        await _cleanerService.DeleteCleanerAsync(id);
        Cleaners.Remove(cleaner);

        NotifyStateChanged();
    }

    /// <summary>
    ///     Notifies subscribers that the state of the cleaners has changed.
    /// </summary>
    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}